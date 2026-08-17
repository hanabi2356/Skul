using System;
using UnityEngine;

public class NormalEnemyView : MonoBehaviour, INormalEnemyView
{
	[SerializeField] private Transform _normalEnemyTransform;
	[SerializeField, Label("발사체 소환 위치"),Tooltip("AttackType이 Range인 경우에만 할당")] private Transform _projectileSpanwTransform;
	[SerializeField, Label("발사체 Addressables 이름"),Tooltip("AttackType이 Range인 경우에만 할당")] private string _projectileAddress;
	private Rigidbody2D _rigidbody;
	private Vector2 _targetPosition;
	private Animator _animator;
	private bool _isAttacking;
	private NormalEnemyAnimEventListener _normalEnemyAnimEventListener;
	private NormalEnemyPhysicsHandler _physicsHandler;

	public Transform NormalEnemyTransform => _normalEnemyTransform;
	public Vector2 TargetPosition => _targetPosition;
	public Rigidbody2D Rigidbody => _rigidbody;
	public Animator Animator => _animator;
	public bool IsAttacking => _isAttacking;
	public NormalEnemyPhysicsHandler PhysicsHandler => _physicsHandler;
	public Vector2 Velocity => _rigidbody != null ? _rigidbody.linearVelocity : Vector2.zero;

	public NormalEnemyAnimEventListener NormalEnemyAnimEventListener
	{
		get
		{
			if (_normalEnemyTransform == null) return null;
			if (_normalEnemyAnimEventListener == null)
			{
				_normalEnemyAnimEventListener =
					_normalEnemyTransform.GetComponentInChildren<NormalEnemyAnimEventListener>();
			}
			return _normalEnemyAnimEventListener;
		}
	}

	public string ProjectileAddress => _projectileAddress;
	public Transform ProjectileSpawnTransform => _projectileSpanwTransform;

	public event Action OnAttack;

	private void Awake()
	{
		if (_normalEnemyTransform == null)
		{
			Debug.LogError("NormalEnemyView: _normalEnemyTransform이 할당되지 않았습니다.", this);
			return;
		}

		_rigidbody = _normalEnemyTransform.GetComponent<Rigidbody2D>();
		_animator = _normalEnemyTransform.GetComponentInChildren<Animator>();
		_physicsHandler = _normalEnemyTransform.GetComponent<NormalEnemyPhysicsHandler>();
	}

	public void SetIsAttacking(bool value) => _isAttacking = value;

	public void SetRotation(bool lookRight)
	{
		if (_isAttacking || _normalEnemyTransform == null) return;

		_normalEnemyTransform.rotation = lookRight
			? Quaternion.identity
			: Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}

	public void SetVelocity(float x, float y)
	{
		if (_rigidbody == null) return;
		_rigidbody.linearVelocity = new Vector2(x, y);
	}

	public void SetVelocityX(float x)
	{
		if (_rigidbody == null) return;
		_rigidbody.linearVelocity = new Vector2(x, _rigidbody.linearVelocity.y);
	}

	public void SetVelocityY(float y)
	{
		if (_rigidbody == null) return;
		_rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, y);
	}

	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}
}
