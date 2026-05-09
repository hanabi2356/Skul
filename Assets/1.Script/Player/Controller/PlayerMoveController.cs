using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMoveController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private float moveSpeed = 5.0f;

    [Header("Jump Setting")]
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private int jumpMaxCount = 2;
    [SerializeField] private float fallMultiply = 2.5f;

    [Header("Dash Setting")]
    [SerializeField] private float dashForce = 5.0f;
    [SerializeField] private float dashCoolDown = 1.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private int dashMaxCount = 2;

    [Header("Debug Value(수정 X)")]
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private bool isJump = true;
    [SerializeField] private int dashCount = 0;
    [SerializeField] private bool isDash = true;
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;

    private Vector2 gazeVector = new Vector2(1.0f, 0.0f); //시선 백터
    
    public bool isDashing { get; private set; } = false;
    
    

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        
    }

    

    void FixedUpdate()
    {
        if (isDashing)
            return;

        PlayerMove();
        JumpCounter();
        MultiplyGravity();
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
            StartCoroutine(Dash());
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
    /// 대쉬 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        //isDash = false;
        isDashing = true;
        playerBase.body.linearVelocity = new Vector2(gazeVector.x*dashForce, 0.0f);

        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        dashCount++;
        
        yield return new WaitForSeconds(dashCoolDown);
        dashCount = 0;
        //isDash = true;
    }
    private void JumpCounter()
    {
        if(jumpCount >= jumpMaxCount)
        {
            isJump = false;
        }

        if(playerBase.physicsHandler.IsGround())
        {
            jumpCount = 0;
            isJump = true;
        }
       
    }
    
}
