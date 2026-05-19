using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
public class PlayerMoveController : MonoBehaviour
{
    
    private float moveSpeed;
    
    private float jumpForce;
    private int jumpMaxCount;
    private float fallMultiply;

    private float dashForce;
    private float dashCoolTime;
    private float dashDuration;
    private int dashMaxCount;

    [SerializeField, Label("코요테 타임")] private float coyoteTime=0.3f;

    [Header("확인용 변수(조작 X)")]
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private bool isJump = true;
    [SerializeField] private int dashCount = 0;
    [field : SerializeField]public bool isDashing { get; private set; } = false;
    [SerializeField]private bool isCoyoteTimeEnd = false;
    [SerializeField]private bool canMove = false;

    private bool isDashCoolDown = false;
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;

    public Vector2 gazeVector { get; private set; } = new Vector2(1.0f, 0.0f); //시선 백터
    
    

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        playerBase.body.gravityScale = 2.5f;
        InitStat();
    }

    void FixedUpdate()
    {
        if (playerBase.attackController.attackCount == 0 && !canMove)
            canMove = true;

        if (isDashing || !canMove)
            return;

        PlayerMove();
        if (playerBase.attackController.attackCount > 0)
        {
            LockMovement();
        }
        
        JumpCounter();
        MultiplyGravity();
        HandleCoyoteTime();
    }
    private void InitStat()
    {
        moveSpeed = playerBase.finalMoveSpeed;

        jumpForce=playerBase.finalJumpForce;
        jumpMaxCount = playerBase.finalJumpMaxCount;
        fallMultiply = playerBase.finalFallMultiply;

        dashForce = playerBase.finalDashForce;
        dashCoolTime = playerBase.finalDashCoolTime;
        dashDuration = playerBase.finalDashDuration;
        dashMaxCount = playerBase.finalDashMaxCount;

        canMove = true;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            gazeVector = moveInput;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && isJump)
        {
            Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started && dashCount < dashMaxCount)
        {
           if(!isDashing)
                StartCoroutine(IEDash());
        }
    }
    /// <summary>
    /// 점프
    /// </summary>
    private void Jump()
    {
        
        playerBase.body.linearVelocity = new Vector2(playerBase.body.linearVelocity.x, jumpForce);
        
        jumpCount++;
    }
    /// <summary>
    /// 점프 후 낙하 시 중력을 추가로 주는 함수
    /// </summary>
    private void MultiplyGravity()
    {
        if(playerBase.body.linearVelocity.y <0.0f)
        {
            //기존 중력에 1이여서 원하는 값을 정확히 계산하기 위해 fallMultiply-1을 함
            playerBase.body.linearVelocity += Vector2.up * Physics2D.gravity * (fallMultiply - 1) * Time.fixedDeltaTime;
        }
    }
    private void PlayerMove()
    {
        if(!playerBase.physicsHandler.IsWallCheck())
        {
            float targetX = moveInput.x*moveSpeed;
            playerBase.body.linearVelocity = new Vector2(targetX, playerBase.body.linearVelocity.y);

        }
        
        transform.rotation = gazeVector.x > 0.0f ? new Quaternion(0.0f, 0.0f, 0.0f, 0.0f) : new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
    }
    private void LockMovement()
    {
        playerBase.body.linearVelocity=Vector2.zero;
        canMove = false;
    }
    /// <summary>
    /// 대쉬 코루틴(이동만 처리)
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEDash()
    {
        //isDash = false;
        isDashing = true;
        dashCount++;

        playerBase.body.linearVelocity = new Vector2(gazeVector.x*(dashForce+moveInput.x), 0.0f);
        
        yield return new WaitForSeconds(dashDuration); //키를 누르지 못하는 시간
        isDashing = false;
        
        if(!isDashCoolDown)
        {
            StartCoroutine(IEDashCoolDown());
        }
    }
    private IEnumerator IEDashCoolDown()
    {
        isDashCoolDown = true;
        while(dashCount > 0)
        {
            yield return new WaitForSeconds(dashCoolTime);
            dashCount = 0;
        }
        isDashCoolDown = false;
    }
    /// <summary>
    /// 공중에 있을 때 점프 횟 수를 1회 증가 시키는 
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEStartCoyoteTime()
    {
        
        yield return new WaitForSeconds(coyoteTime);
        isCoyoteTimeEnd = true;
        
    }
    private void HandleCoyoteTime()
    {
        if (!playerBase.physicsHandler.IsGround() && !isCoyoteTimeEnd)
            StartCoroutine(IEStartCoyoteTime());

        if (playerBase.physicsHandler.IsGround() && isCoyoteTimeEnd)
            isCoyoteTimeEnd = false;
    }
    private void JumpCounter()
    {
        if(jumpCount >= jumpMaxCount)
        {
            isJump = false;
        }

        if(playerBase.physicsHandler.IsGround()&&playerBase.body.linearVelocity.y<=0.1f)
        {
            jumpCount = 0;
            isJump = true;
        }
        if (isCoyoteTimeEnd)
        {
            jumpCount = 1;
        }
    }
    
}
