using UnityEngine;

public class BossSkillMeleeAttack : BossSkillBase
{

	public BossSkillMeleeAttack(IBossEnemyView view, IEnemyStatModel statModel) : base(view, statModel)
	{
	}

	//변수들은 추후 데이터 파일로 관리할 예정
	public override int SkillNumber => 1;
	protected override float CoolTime => 2.0f;
	public override float MaxUseRange => 3.0f;
	
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
	}

	protected override void OnExit()
	{
		
	}

	
}
