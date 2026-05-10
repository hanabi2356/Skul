using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
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

    [Header("Debug Value(수정 X)")]
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private bool isJump = true;
    [SerializeField] private int dashCount = 0;
    [field : SerializeField]public bool isDashing { get; private set; } = false;

    private bool isDashCoolDown = false;
    
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;

    private Vector2 gazeVector = new Vector2(1.0f, 0.0f); //시선 백터
    
    
    

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        playerBase.body.gravityScale = 2.5f;
        InitStat();
    }

    

    void FixedUpdate()
    {
        if (isDashing)
            return;

        PlayerMove();
        JumpCounter();
        MultiplyGravity();
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
        float targetX = moveInput.x*moveSpeed;
        playerBase.body.linearVelocity = new Vector2(targetX, playerBase.body.linearVelocity.y);
        
        transform.rotation = gazeVector.x > 0.0f ? new Quaternion(0.0f, 0.0f, 0.0f, 0.0f) : new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
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
            dashCount--;
        }
        isDashCoolDown = false;
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
        
    }
    
}
