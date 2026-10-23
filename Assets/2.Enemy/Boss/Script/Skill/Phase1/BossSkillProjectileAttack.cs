using UnityEngine;

public class BossSkillProjectileAttack : BossSkillBase
{
	public BossSkillProjectileAttack(IBossEnemyView view, IEnemyStatModel statModel) : base(view, statModel)
	{
	}

	public override int SkillNumber => 4;

	protected override float CoolTime => 8.0f;
	public override float MaxUseRange => 10.0f;

	protected override bool OnApplyHit()
	{
		throw new System.NotImplementedException();
	}

	
}
