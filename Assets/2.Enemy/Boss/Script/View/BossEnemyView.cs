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

	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}
}
