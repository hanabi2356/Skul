using UnityEngine;

public class BossSkillJumpAttack : BossSkillBase
{
	public BossSkillJumpAttack(IBossEnemyView view, IEnemyStatModel statModel) : base(view, statModel)
	{
	}

	public override int SkillNumber => 2;
	protected override float CoolTime => 5.0f;
	public override float MaxUseRange => 3.0f;

	private Vector2 _jumpStart;
	private Vector2 _jumpTarget;
	private float _jumpDuration;
	private float _jumpElapsed;
	private bool _isJumping;
	private const float _jumpHeight = 3f;
	private float _originGravityScale;

	protected override bool OnApplyHit()
	{
		if (_view.BossEnemyTransform == null) return false;

		Vector2 size = new Vector2(4.0f, 0.2f);
		Vector2 origin = (Vector2)_view.BossEnemyTransform.transform.position;

		Collider2D[] hits = Physics2D.OverlapBoxAll(origin, size, 0.0f, _targetLayer);
		return TryApplyDamage(hits);
	}

	protected override void OnEnter()
	{
		FaceTarget();
		_originGravityScale = _view.Rigidbody.gravityScale;
		BeginJump(3.0f);
	}

	protected override void OnTick()
	{
		if (_isJumping == false) return;

		_jumpElapsed += Time.deltaTime;
		float u = Mathf.Clamp01(_jumpElapsed / _jumpDuration);

		float x = Mathf.Lerp(_jumpStart.x, _jumpTarget.x, u);
		float y = Mathf.Lerp(_jumpStart.y, _jumpTarget.y, u);
		y += _jumpHeight * 4.0f * u * (1.0f - u);

		_view.Rigidbody.MovePosition(new Vector2(x,y));
		
		if(u >= 1.0f)
		{
			_isJumping = false;
			_view.Rigidbody.gravityScale = _originGravityScale;
		}
	}

	private void BeginJump(float jumpTime)
	{
		_jumpStart = _view.BossEnemyTransform.position;
		_jumpTarget = _view.TargetPosition;
		_jumpDuration = Mathf.Max(0.01f, jumpTime);
		_jumpElapsed = 0.0f;
		_isJumping = true;
		_view.Rigidbody.gravityScale = 0.0f;
	}
}
