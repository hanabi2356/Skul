using UnityEngine;

public class NormalEnemyFSMMachine : IFSMMachine, INormalEnemyStateContext
{
	public IState CurrentState { get; private set; }

	public ENormalEnemyState CurrentStateEnum {get; private set;}

	public NormalEnemyIdleState IdleState {get; private set;}

	public NormalEnemyPatrolState PatrolState { get; private set; }

	public NormalEnemyTraceState TraceState { get; private set; }

	public NormalEnemyAttackState AttackState { get; private set; }

	public NormalEnemyHitState HitState { get; private set; }

	public NormalEnemyDeadState DeadState { get; private set; }
	public NormalEnemyFSMMachine(INormalEnemyStatModel statModel, INormalEnemyView view)
	{
		InitState(statModel, view, this);
	}

	public void ChangeState(IState state, ENormalEnemyState stateEnum)
	{
		if (state == null || CurrentState == state) return;
		CurrentState.Exit();

		CurrentState = state;
		CurrentStateEnum = stateEnum;

		CurrentState.Enter();

	}
	public void BootUp()
	{
		SetupAllStateTransition();

		CurrentState = IdleState;
		CurrentStateEnum = ENormalEnemyState.Idle;

		CurrentState.Enter();
	}
	private void InitState(INormalEnemyStatModel statModel, INormalEnemyView view, INormalEnemyStateContext stateContext)
	{
		IdleState = new NormalEnemyIdleState(statModel, view, stateContext);
		PatrolState = new NormalEnemyPatrolState(statModel, view, stateContext);
		TraceState = new NormalEnemyTraceState(statModel, view, stateContext);
		AttackState = new NormalEnemyAttackState(statModel, view, stateContext);
		HitState = new NormalEnemyHitState(statModel, view, stateContext);	
		DeadState = new NormalEnemyDeadState(statModel, view, stateContext);

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
		if(state is NormalEnemyBaseState baseState)
		{
			baseState.SetupTransitions();
		}
	}
}
