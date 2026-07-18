using UnityEngine;

public class NormalEnemyTraceState : NormalEnemyBaseState
{
	public NormalEnemyTraceState(INormalEnemyStatModel normalEnemyStatModel,
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
