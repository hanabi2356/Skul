using UnityEngine;

public class BossSkillDashAttack : BossSkillBase
{
	public BossSkillDashAttack(IBossEnemyView view, IEnemyStatModel statModel) : base(view, statModel)
	{
	}

	public override int SkillNumber => 3;
	protected override float CoolTime => 3.0f;
	public override float MaxUseRange => 6.0f;

	private const float _dashDistance = 8.0f;
	private const float _dashSpeed = 16.0f;

	private bool _isDash;
	private float _dashElapsed;
	private float _dashDuration;
	private float _dashStartX;
	private float _dashEndX;

	
	protected override bool OnApplyHit()
	{

		if (_view.BossEnemyTransform == null) return false;

		float dir = _view.LookDirection;
		Vector2 size = new Vector2(2.0f, 1.5f);
		Vector2 offset = new Vector2(size.x * 0.5f * dir, 0.5f);
		Vector2 origin = (Vector2)_view.BossEnemyTransform.position + offset;

		Collider2D[] hits = Physics2D.OverlapBoxAll(origin, size, 0.0f, _targetLayer);
		return TryApplyDamage(hits);
	}

	protected override void OnEnter()
	{
		base.OnEnter();
		BeginDash();
	}
	protected override void OnTick()
	{
		if (_isDash == false) return;
		if (_view.Rigidbody == null) return;

		_dashElapsed += Time.deltaTime;
		float u = Mathf.Clamp01(_dashElapsed / _dashDuration);
		float x = Mathf.Lerp(_dashStartX, _dashEndX, u);

		_view.Rigidbody.MovePosition(new Vector2(x, _view.Rigidbody.position.y));

		if(u >= 1)
		{
			_isDash = false;
			RequestedEnd();
		}
	}
	protected override void OnExit()
	{
		_isDash = false;
	}
	protected override void OnCancel()
	{
		_isDash = false;
	}
	private void BeginDash()
	{
		if (_view.BossEnemyTransform == null || _view.Rigidbody == null) return;


		_dashStartX = _view.Rigidbody.position.x;
		_dashEndX = _dashStartX + _view.LookDirection * _dashDistance;
		_dashDuration = _dashDistance / _dashSpeed;
		_isDash = true;
		_dashElapsed = 0.0f;

	}
}
