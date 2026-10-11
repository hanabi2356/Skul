using UnityEngine;

public class BossEnemyAttackState : BossEnemyBaseState
{
	public BossEnemyAttackState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext) : base(statModel, view, stateContext)
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
		_trasitions.Add(new BossEnemyTrasition(_stateContext.IdleState, EBossEnemyState.Idle,
			() => _view.UseSkill == false));

		_trasitions.Add(new BossEnemyTrasition(_stateContext.DeadState, EBossEnemyState.Dead,
			() => _statModel.IsDead));
	}

	
}
