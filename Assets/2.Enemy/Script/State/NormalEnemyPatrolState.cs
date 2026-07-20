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
		throw new System.NotImplementedException();
	}
	public override void Execute()
	{
		base.Execute();
	}
	public override void Exit()
	{
		throw new System.NotImplementedException();
	}

	public override void SetupTransitions()
	{
		throw new System.NotImplementedException();
	}
}
