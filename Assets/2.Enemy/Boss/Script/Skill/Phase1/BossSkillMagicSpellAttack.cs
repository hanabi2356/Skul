using UnityEngine;

public class BossSkillMagicSpellAttack : BossSkillBase
{
	public BossSkillMagicSpellAttack(IBossEnemyView view, IEnemyStatModel statModel) : base(view, statModel)
	{
	}

	public override int SkillNumber => 5;
	protected override float CoolTime => 20.0f;

	private int _attackCount = 3;
	private Vector2 _startPosition;
	private Vector2 _movePosition;
	private bool _isMoving;
	private float _moveElapsed;
	private const float _moveDuration = 0.4f;


	protected override bool OnApplyHit()
	{
		Vector2 position = _view.BossEnemyTransform.position;
			
		return true;
	}

	protected override void OnEnter()
	{
		base.OnEnter();
	}

	protected override void OnTick()
	{

		if (_isMoving == false || _view.Rigidbody == null) return;
		_moveElapsed += Time.deltaTime;
		float u = Mathf.Clamp01(_moveElapsed / _moveDuration);
		_view.Rigidbody.MovePosition(Vector2.Lerp(_startPosition, _movePosition, u));
		if (u >= 1f)
		{
			_isMoving = false;
			_view.Rigidbody.gravityScale = 1f; // 원래 값으로 되돌리기
			RequestedEnd();
		}
	}

	protected override void OnExit()
	{
		base.OnExit();
	}
}
