using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMoveController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float dashForce = 5.0f;
    [SerializeField] private int jumpMaxCount = 2;
    [SerializeField] private float dashCoolDown = 1.0f;
    [SerializeField] private float fallMultiply = 2.5f;
    [Header("Debug Value(수정 X)")]
    [SerializeField]private int jumpCount = 0;
    [SerializeField] private bool isJump = true;
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;

    private Vector2 gazeVector = new Vector2(1.0f, 0.0f); //시선 백터
    private bool isDash = true;

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        
    }

    

    void FixedUpdate()
    {
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
        if(context.started && isDash)
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

    private IEnumerator Dash()
    {
        Debug.Log("Dash");
        isDash = false;
        playerBase.body.linearVelocity = new Vector2(gazeVector.x*dashForce, playerBase.body.linearVelocity.y);

        yield return new WaitForSeconds(dashCoolDown);
        isDash = true;
        
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
