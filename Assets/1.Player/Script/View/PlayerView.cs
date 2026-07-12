using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour, IPlayerView
{
	[SerializeField] private Transform _playerTransform;
	private Rigidbody2D _rigidbody;
	private PlayerPhysicsHandler _physicsHandler;
	private Animator _animator;
	private PlayerAnimEventListener _playerAnimEventListener;

	private bool _isAttacking;
	private bool _canAttackDash;

	public event Action<Vector2> OnMove;
	public event Action OnJump;
	public event Action OnDash;
	public event Action OnAttack;
	public event Action OnPlatformIgnore;

	public Rigidbody2D Rigidbody => _rigidbody;
	
	private float _originalGravityScale;
	public float CurrentVelocityY => Rigidbody.linearVelocity.y;
	public PlayerPhysicsHandler PhysicsHandler => _physicsHandler;

	public Animator Animator => _animator;

	public bool IsAttacking => _isAttacking;

	public bool CanAttackDash => _canAttackDash;

	public Transform PlayerTransform => _playerTransform;
	public PlayerAnimEventListener PlayerAnimEventListener
	{
		get
		{
			if (_playerTransform == null)
			{
				Debug.Log("_playerTransform is null");
				return null;
			}
			if (_playerAnimEventListener == null)
			{
				_playerAnimEventListener = _playerTransform.GetComponentInChildren<PlayerAnimEventListener>();
			}
			return _playerAnimEventListener;
		}
	}

	

	void Start()
    {
        
    }
	private void Awake()
	{
		if(_playerTransform == null)
		{
			Debug.Log("인스펙터 할당 안함");
			return;
		}

		_rigidbody = _playerTransform.GetComponent<Rigidbody2D>();
		_physicsHandler = _playerTransform.GetComponent<PlayerPhysicsHandler>();
		_animator = _playerTransform.GetComponentInChildren<Animator>();
		_playerAnimEventListener = _playerTransform.GetComponentInChildren<PlayerAnimEventListener>();

		_originalGravityScale = _rigidbody.gravityScale;

	}
	void Update()
    {
        

    }
	public void AddImpulse(Vector2 impulse)
	{

		_rigidbody.AddForce(impulse, ForceMode2D.Impulse);
	}

	/// <summary>
	/// 키를 누르거나 땔 때만 호출됨
	/// </summary>
	/// <param name="context"></param>
	public void InputMoveVector(InputAction.CallbackContext context)
	{
		Vector2 input = context.ReadValue<Vector2>();
		OnMove?.Invoke(input);
	}
	public void InputJump(InputAction.CallbackContext context)
	{
		if (context.started )
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
		if(context.started )
		{
			OnAttack?.Invoke();
		}
	}

	public void InputPlatformIgnore(InputAction.CallbackContext context)
	{
		if(context.started)
		{
			OnPlatformIgnore?.Invoke();
		}
	}
	public void SetVelocityX(float x)
	{
		Rigidbody.linearVelocity = new Vector2(x, Rigidbody.linearVelocity.y);
	}
	public void SetVelocityY(float y)
	{
		Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, y);
	}
	public void SetVelocity(float x, float y)
	{
		Rigidbody.linearVelocity = new Vector2(x, y);
	}
	public void SetGravityScale(bool isDash)
	{
		Rigidbody.gravityScale = isDash ? 0.0f:_originalGravityScale;
	}
	public void SetRotation(bool lookRight)
	{
		if (IsAttacking) return;
		
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

	public void SetIsAttacking(bool value) => _isAttacking = value;

	public void AttackDash(float force)
	{
		_playerTransform.position += new Vector3(force, 0.0f, 0.0f);
	}

	public void SetCanAttackDash(bool value) => _canAttackDash = value;


}
