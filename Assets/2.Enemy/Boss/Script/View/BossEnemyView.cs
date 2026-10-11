using System;
using UnityEngine;

public class BossEnemyView : MonoBehaviour, IBossEnemyView
{
	[SerializeField] private Transform _bossEnemyTransform;
	private Animator _animator;
	private Rigidbody2D _rigidbody;
	private bool _useSkill;
	private EnemyPhysicsHandler _physicsHandler;
	private Vector2 _targetPosition;

	public Transform BossEnemyTransform => _bossEnemyTransform;
	public event Action OnSkill;
	public Animator Animator => _animator;
	public Rigidbody2D Rigidbody => _rigidbody;
	public bool UseSkill => _useSkill;
	public EnemyPhysicsHandler PhysicsHandler => _physicsHandler;
	public Vector2 TargetPosition => _targetPosition;

	private void Awake()
	{
		if (_bossEnemyTransform == null)
		{
			Debug.LogError("BossEnemyView: _bossEnemyTransform이 할당되지 않았습니다.", this);
			return;
		}

		_rigidbody = _bossEnemyTransform.GetComponent<Rigidbody2D>();
		_animator = _bossEnemyTransform.GetComponentInChildren<Animator>();
		_physicsHandler = _bossEnemyTransform.GetComponent<EnemyPhysicsHandler>();
	}

	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	public void SetRotation(bool lookRight)
	{
		if (_bossEnemyTransform == null) return;
		if (_useSkill) return;

		_bossEnemyTransform.rotation = lookRight
			? Quaternion.identity
			: Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}

	public void SetUseSkill(bool useSkill)
	{
		_useSkill = useSkill;
	}
}
