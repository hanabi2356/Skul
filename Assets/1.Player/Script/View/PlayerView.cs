using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour, IPlayerView
{
	[SerializeField]private Transform _playerTransform;
	[SerializeField] private Rigidbody2D _body;
	[SerializeField] private PlayerPhysicsHandler _physicsHandler;

	public event Action<Vector2> OnMove;
	public event Action OnJump;
	public event Action OnDash;
	public event Action OnAttack;
	public Rigidbody2D Body => _body;
	
	private float _originalGravityScale;
	public float CurrentVelocityY => Body.linearVelocity.y;
	public PlayerPhysicsHandler PhysicsHandler => _physicsHandler;


	void Start()
    {
        
    }
	private void Awake()
	{
		_playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		_body = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		_originalGravityScale = Body.gravityScale;
		_physicsHandler = _playerTransform.GetComponent<PlayerPhysicsHandler>();
	}
	void Update()
    {
        

    }
	public void AddVelocity(Vector3 velocity)
	{
		throw new System.NotImplementedException();
	}

	public void InputMoveVector(InputAction.CallbackContext context)
	{
		Vector2 input = context.ReadValue<Vector2>();
		OnMove?.Invoke(input);
	}
	public void InputJump(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			OnJump?.Invoke();
		}
	}
	public void InputDash(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			
			OnDash?.Invoke();
		}
	}
	public void InputAttack(InputAction.CallbackContext context)
	{
		if(context.started)
		{
			OnAttack?.Invoke();
		}
	}
	public void SetVelocityX(float x)
	{
		Body.linearVelocity = new Vector2(x, Body.linearVelocity.y);
	}
	public void SetVelocityY(float y)
	{
		Body.linearVelocity = new Vector2(Body.linearVelocity.x, y);
	}
	public void SetVelocity(float x, float y)
	{
		Body.linearVelocity = new Vector2(x, y);
	}
	public void SetGravityScale(bool isDash)
	{
		Body.gravityScale = isDash ? 0.0f:_originalGravityScale;
	}
	public void SetRotation(bool lookRight)
	{
		if (lookRight)
		{
			_playerTransform.rotation = Quaternion.identity;
		}
		else
		{
			_playerTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
		}
	}
	public void SetOneWayPlatformCollision(bool ignore)
	{
		PhysicsHandler.SetOneWayPlatformIgnore(ignore);
	}

	
}
