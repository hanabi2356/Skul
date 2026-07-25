using System;
using UnityEngine;

public class NormalEnemyView : MonoBehaviour, INormalEnemyView
{
	[SerializeField] private Transform _normalEnemyTransform;
	public Transform NormalEnemyTransform => _normalEnemyTransform;

	private Vector2 _targetPosition;
	public Vector2 TargetPosition => _targetPosition;

	private Rigidbody2D _rigidbody;
	public Rigidbody2D Rigidbody => _rigidbody;

	private Animator _animator;
	public Animator Animator => _animator;

	private bool _isAttacking = false;
	public bool IsAttacking => _isAttacking;

	private NormalEnemyAnimEventListener _normalEnemyAnimEventListener;
	public NormalEnemyAnimEventListener NormalEnemyAnimEventListener
	{
		get
		{
			if (_normalEnemyTransform == null)
			{
				return null;
			}
			if ( _normalEnemyAnimEventListener == null )
			{
				_normalEnemyAnimEventListener = _normalEnemyTransform.GetComponentInChildren<NormalEnemyAnimEventListener>();
			}
			return _normalEnemyAnimEventListener;
		}
	}


	public event Action OnAttack;


	private void Awake()
	{
		_rigidbody = _normalEnemyTransform.GetComponent<Rigidbody2D>();
		_animator = _normalEnemyTransform.GetComponentInChildren<Animator>();
		
	}
	public void SetIsAttacking(bool value)
	{
		_isAttacking = value;
	}

	public void SetRotation(bool lookRight)
	{
		if (_isAttacking) return;
		if(lookRight)
		{
			_normalEnemyTransform.rotation = Quaternion.identity;
		}
		else
		{
			_normalEnemyTransform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
		}
	}

	public void SetVelocity(float x, float y)
	{
		_rigidbody.linearVelocity = new Vector2(x, y);
	}

	public void SetVelocityX(float x)
	{
		_rigidbody.linearVelocity = new Vector2(x, _rigidbody.linearVelocity.y);
	}
	public void SetVelocityY(float y)
	{
		_rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, y);
	}
	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	
}
