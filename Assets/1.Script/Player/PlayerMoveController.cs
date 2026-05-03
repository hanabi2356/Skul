using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private float mMoveSpeed = 5.0f;

    private Vector2 mMoveInput;
    private Rigidbody2D mBody;


    void Awake()
    {
        mBody = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mMoveInput = context.ReadValue<Vector2>();
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
        Vector2 moveVector = new Vector2(mMoveInput.x, mMoveInput.y) * mMoveSpeed * Time.fixedDeltaTime;
        Vector2 currPos = mBody.position;
        Vector2 deltaPosition = currPos + moveVector;

        mBody.MovePosition(deltaPosition);
    }
    private void Dash()
    {
        Debug.Log("Dash Call");
    }
}
