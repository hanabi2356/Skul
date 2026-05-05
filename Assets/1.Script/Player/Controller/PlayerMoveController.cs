using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMoveController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    public Vector2 moveInput { get; private set; }

    private PlayerBase playerBase;


    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();

    }

    

    void FixedUpdate()
    {
        PlayerMove();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Jump();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Dash();
        }
    }
    private void Jump()
    {
        Debug.Log("Jump");
        playerBase.body.linearVelocity = new Vector2(playerBase.body.linearVelocity.x, 0.0f);
        playerBase.body.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
    }

    private void PlayerMove()
    {
        float targetX = moveInput.x*moveSpeed;
        playerBase.body.linearVelocity = new Vector2(targetX, playerBase.body.linearVelocity.y);
    }
    private void Dash()
    {
        Debug.Log("Dash Call");
    }
    private void ApplyInputValue()
    {
        
    }
   
}
