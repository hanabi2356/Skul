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


	protected override bool OnApplyHit()
	{
		throw new System.NotImplementedException();
	}
}
