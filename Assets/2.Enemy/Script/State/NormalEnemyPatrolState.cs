using UnityEngine;

public class NormalEnemyPatrolState : NormalEnemyBaseState
{
	public NormalEnemyPatrolState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext) : base(normalEnemyStatModel, view, stateContext)
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
