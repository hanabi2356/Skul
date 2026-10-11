using UnityEngine;

public class BossEnemyIdleState : BossEnemyBaseState
{
	public BossEnemyIdleState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext) : base(statModel, view, stateContext)
	{
	}

	public override void Enter()
	{
	}
	public override void Execute()
	{
		base.Execute();
	}

	public override void Exit()
	{
	}

	public override void SetupTransitions()
	{
		_trasitions.Add(new BossEnemyTrasition(_stateContext.AttackState, EBossEnemyState.Attack,
			() => _view.UseSkill));

		_trasitions.Add(new BossEnemyTrasition(_stateContext.DeadState, EBossEnemyState.Dead,
			() => _statModel.IsDead));

	}
}
