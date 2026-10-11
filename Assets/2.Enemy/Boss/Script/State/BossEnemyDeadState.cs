using UnityEngine;

public class BossEnemyDeadState : BossEnemyBaseState
{
	public BossEnemyDeadState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext) : base(statModel, view, stateContext)
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
