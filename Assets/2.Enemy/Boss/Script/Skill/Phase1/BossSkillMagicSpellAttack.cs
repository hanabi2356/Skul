using UnityEngine;

public class BossSkillMagicSpellAttack : BossSkillBase
{
	private readonly BossSkillView _skillView;
	public BossSkillMagicSpellAttack(IBossEnemyView view, IEnemyStatModel statModel, BossSkillView skillView) : base(view, statModel)
	{
		_skillView = skillView;
	}

	public override int SkillNumber => 5;
	protected override float CoolTime => 20.0f;
	public override float MaxUseRange => 30.0f;

	private int _attackCount = 3;
	private int _laserCount = 5;
	private float _attackPointRadius = 5.0f;
	private const float _laserThickness = 0.4f;

	private Vector2 _startPosition;
	private Vector2 _movePosition;
	private bool _isMoving;
	private bool _isAttacking;
	private float _moveElapsed;
	private const float _moveDuration = 0.4f;
	private const float _attackWaitTime = 1.0f;
	private const float _laserLifeTime = 0.45f;
	private const float _laserLength = 15.0f;
	private int _wave;
	private float _waveTimer;
	private float _originGravityScale;
	private bool _gravityChanged;
	private bool _waitLastLaser;
	private GameObject _center;

	protected override bool OnApplyHit()
	{
		
		
		return true;
	}

	protected override void OnEnter()
	{
		_center = null;
		_isMoving = false;
		_isAttacking = false;
		_wave = 0;
		_gravityChanged = false;
		if (_view.BossEnemyTransform == null || _view.Rigidbody == null)
		{
			RequestedEnd();
			return;
		}
		MagicSpellSettings settings = _skillView.MagicSpell;
		if (settings.SpellPoint == null || settings.CenterPrefab == null)
		{
			RequestedEnd();
			return;
		}
		_originGravityScale = _view.Rigidbody.gravityScale;
		_view.Rigidbody.gravityScale = 0f;
		_gravityChanged = true;
		_startPosition = _view.BossEnemyTransform.position;
		_movePosition = settings.SpellPoint.position;
		_moveElapsed = 0f;
		_isMoving = true;
	}

	protected override void OnTick()
	{
		if (_isMoving)
			TickMove();
		if (_isAttacking)
			TickAttack();
	}

	protected override void OnExit()
	{
		Release();
	}

	protected override void OnCancel()
	{
		Release();
	}

	private void TickMove()
	{
		if (_view.Rigidbody == null)
		{
			_isMoving = false;
			RequestedEnd();
			return;
		}

		_moveElapsed += Time.deltaTime;
		float u = Mathf.Clamp01(_moveElapsed / _moveDuration);
		_view.Rigidbody.MovePosition(Vector2.Lerp(_startPosition, _movePosition, u));

		if (u < 1f) return;

		_isMoving = false;
		BeginAttack();
	}

	private void BeginAttack()
	{
		_center = Object.Instantiate(_skillView.MagicSpell.CenterPrefab, _movePosition, Quaternion.identity);
		_wave = 0;
		_waveTimer = 0f;
		_waitLastLaser = false;
		_isAttacking = true;
	}

	private void TickAttack()
	{
		if (_center == null)
		{
			_isAttacking = false;
			RequestedEnd();
			return;
		}

		_waveTimer -= Time.deltaTime;
		if (_waveTimer > 0f) return;

		if(_waitLastLaser)
		{
			_isAttacking = false;
			RequestedEnd();
			return;
		}

		FireWave();
		_wave++;
		if (_wave >= _attackCount)
		{
			_waitLastLaser = true;
			_waveTimer = _laserLifeTime;
			return;
		}

		_waveTimer = _attackWaitTime;
	}

	private void FireWave()
	{
		GameObject laserPrefab = _skillView.MagicSpell.LaserPrefab;

		if (_center == null || laserPrefab == null) return;

		Vector2 origin = _center.transform.position;
		Vector2[] points = AttackPointCalculate(origin, _laserCount);

		for (int i = 0; i < points.Length; i++)
		{
			Vector2 offset = points[i] - origin;

			if (offset.sqrMagnitude <= 0.0001f) continue;

			Vector2 direction = offset.normalized;
			GameObject laserObject = Object.Instantiate(laserPrefab, origin, Quaternion.identity, _center.transform);
			MagicSpellLaser laser = laserObject.GetComponent<MagicSpellLaser>();

			if (laser == null) continue;

			laser.Initialize(_statModel.FinalDamage, direction, origin, _laserLength, _laserThickness, _targetLayer, _laserLifeTime);
		}
	}

	private Vector2[] AttackPointCalculate(Vector2 origin, int size)
	{
		Vector2[] points = new Vector2[size];
		for (int i = 0; i < size; i++)
		{
			points[i] = origin + Random.onUnitCircle * _attackPointRadius;
		}
		return points;
	}

	private void Release()
	{
		if (_gravityChanged && _view.Rigidbody != null)
		{
			_view.Rigidbody.gravityScale = _originGravityScale;
			_gravityChanged = false;
		}

		if (_center != null)
		{
			Object.Destroy(_center);
			_center = null;
		}

		_isMoving = false;
		_isAttacking = false;
	}
}
