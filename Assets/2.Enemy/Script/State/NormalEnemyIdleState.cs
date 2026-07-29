using UnityEngine;

public class NormalEnemyIdleState : NormalEnemyBaseState
{
	public NormalEnemyIdleState(INormalEnemyStatModel normalEnemyStatModel,
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
		// TODO: DetectRange 안이면 Trace로 전환
	}
}
