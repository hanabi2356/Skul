using System.Collections.Generic;
using UnityEngine;
public enum ENormalState
{
	Idle = 0,
	Patrol = 1,
	Trace = 2,
	Attack = 3,
	Hit = 4,
	Dead = 5
}

public abstract class NormalEnemyBaseState : IState
{
	protected INormalEnemyStatModel _normalEnemyStatModel;
	protected INormalEnemyView _view;
	protected INormalEnemyStateContext _stateContext;

	protected List<IPlayerTransition> _transitions = new List<IPlayerTransition>();

	protected NormalEnemyBaseState(INormalEnemyStatModel normalEnemyStatModel, 
		INormalEnemyView view, 
		INormalEnemyStateContext stateContext)
	{
		_normalEnemyStatModel = normalEnemyStatModel;
		_view = view;
		_stateContext = stateContext;
	}

	public abstract void Enter();


	public virtual void Execute()
	{

	}


	public abstract void Exit();


	public abstract void SetupTransitions();
	
}
