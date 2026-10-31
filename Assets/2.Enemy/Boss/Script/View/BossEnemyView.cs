using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;

public class BossEnemyView : MonoBehaviour, IBossEnemyView
{
	[SerializeField] private Transform _bossEnemyTransform;
	[SerializeField] private LayerMask _targetLayer;

	private Animator _animator;
	private Rigidbody2D _rigidbody;
	private bool _useSkill;
	private EnemyPhysicsHandler _physicsHandler;
	private Vector2 _targetPosition;
	private BossEnemyAnimEventListener _bossAnimEventListener;
	private int _phase = 1;
	private bool _lookRight = true;

	public Transform BossEnemyTransform => _bossEnemyTransform;

	public event Action OnSkill;

	public Animator Animator => _animator;

	public Rigidbody2D Rigidbody => _rigidbody;

	public bool UseSkill => _useSkill;

	public EnemyPhysicsHandler PhysicsHandler => _physicsHandler;

	public Vector2 TargetPosition => _targetPosition;

	public BossEnemyAnimEventListener AnimEventListener
	{
		get
		{
			if (_bossEnemyTransform == null) return null;
			if(_bossAnimEventListener == null)
			{
				_bossAnimEventListener = _bossEnemyTransform.GetComponentInChildren<BossEnemyAnimEventListener>();
			}
			return _bossAnimEventListener;
		}
	}

	public LayerMask TargetLayer => _targetLayer;

	public int CurrentPhase => _phase;

	public bool LookRight => _lookRight;

	public float LookDirection => _lookRight ? 1.0f : -1.0f;


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
	public void SetRotation(bool lookRight)
	{
		if (_bossEnemyTransform == null) return;
		if (_useSkill) return;

		_lookRight = lookRight;
		_bossEnemyTransform.rotation = lookRight
			? Quaternion.identity
			: Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}

	public void SetUseSkill(bool useSkill)
	{
		_useSkill = useSkill;
	}
	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	public void SetPhase(int phase)
	{
		_phase = phase;
	}
}
