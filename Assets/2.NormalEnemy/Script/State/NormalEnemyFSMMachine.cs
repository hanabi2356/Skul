public class NormalEnemyFSMMachine : FSMMachine<ENormalEnemyState>, INormalEnemyStateContext
{

	public NormalEnemyIdleState IdleState { get; private set; }
	public NormalEnemyPatrolState PatrolState { get; private set; }
	public NormalEnemyTraceState TraceState { get; private set; }
	public NormalEnemyAttackState AttackState { get; private set; }
	public NormalEnemyHitState HitState { get; private set; }
	public NormalEnemyDeadState DeadState { get; private set; }

	public NormalEnemyFSMMachine(INormalEnemyStatModel statModel,
		INormalEnemyView view,
		NormalEnemyRangeDetectionController rangeController,
		NormalEnemyMoveController moveController,
		NormalEnemyAttackController attackController)
	{
		InitState(statModel, view, this, rangeController, moveController, attackController);
	}

	

	public void BootUp()
	{
		base.BootUp(IdleState, ENormalEnemyState.Idle, IdleState, PatrolState, TraceState, AttackState, HitState, DeadState);
	}

	private void InitState(INormalEnemyStatModel statModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyRangeDetectionController rangeController,
		NormalEnemyMoveController moveController,
		NormalEnemyAttackController attackController)
	{
		IdleState = new NormalEnemyIdleState(statModel, view, stateContext, rangeController, attackController);
		PatrolState = new NormalEnemyPatrolState(statModel, view, stateContext, moveController, rangeController, attackController);
		TraceState = new NormalEnemyTraceState(statModel, view, stateContext, rangeController, moveController, attackController);
		AttackState = new NormalEnemyAttackState(statModel, view, stateContext, rangeController, moveController, attackController);
		HitState = new NormalEnemyHitState(statModel, view, stateContext, moveController, attackController, rangeController);
		DeadState = new NormalEnemyDeadState(statModel, view, stateContext, moveController, attackController);
	}

	private void SetupAllStateTransition()
	{
		TrySetup(IdleState);
		TrySetup(PatrolState);
		TrySetup(TraceState);
		TrySetup(AttackState);
		TrySetup(HitState);
		TrySetup(DeadState);
	}

	private void TrySetup(IState state)
	{
		if (state is NormalEnemyBaseState baseState)
		{
			baseState.SetupTransitions();
		}
	}
}
