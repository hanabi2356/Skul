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

    [Header("Debug Value(수정 X)")]
    [SerializeField]private int jumpCount = 0;
    [SerializeField] private bool isJump = true;
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;

    private Vector2 gazeVector = new Vector2(1.0f, 0.0f); //시선 백터

    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        
    }

    

    void FixedUpdate()
    {
        PlayerMove();
        JumpCounter();
        Debug.Log(gazeVector);
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
        if(context.started)
        {
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        playerBase.body.linearVelocity = new Vector2(playerBase.body.linearVelocity.x, 0.0f);
        playerBase.body.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
        jumpCount++;
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
        playerBase.body.linearVelocity = new Vector2(playerBase.body.linearVelocity.x, playerBase.body.linearVelocity.y);
        playerBase.body.AddForce(transform.right*dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashCoolDown);
        
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
