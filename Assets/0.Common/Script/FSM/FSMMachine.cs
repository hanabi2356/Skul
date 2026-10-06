using System;


public class FSMMachine<TStateEnum> : IFSMMachine, IStateContext<TStateEnum> where TStateEnum : struct, Enum
{
	public IState CurrentState {get; private set;}
	public TStateEnum CurrentStateEnum { get; private set;}

	public void ChangeState(IState state, TStateEnum stateEnum)
	{
		if (state == null || CurrentState == state) return;

		CurrentState?.Exit();

		CurrentState = state;
		CurrentStateEnum = stateEnum;

		CurrentState.Enter();
	}

	protected void BootUp(IState initState, TStateEnum initStateEnum, params IState[] states)
	{
		foreach (var state in states)
		{
			state?.SetupTransitions();
		}

		CurrentState = initState;
		CurrentStateEnum = initStateEnum;
		CurrentState.Enter();
	}
	
}
