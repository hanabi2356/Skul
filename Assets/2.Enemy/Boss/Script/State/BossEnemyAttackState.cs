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
	}

	
}
