using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMoveController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    public Vector2 moveInput { get; private set; }

    private Rigidbody2D body;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        

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
    }

    private void PlayerMove()
    {
        Vector2 moveVector = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        Vector2 currPos = body.position;
        Vector2 deltaPosition = currPos + moveVector;

        body.MovePosition(deltaPosition);
    }
    private void Dash()
    {
        Debug.Log("Dash Call");
    }
    private void ApplyMoveSpeed()
    {
        
    }
   
}
