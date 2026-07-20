using System.Collections.Generic;
using UnityEngine;


public abstract class NormalEnemyBaseState : IState
{
	protected INormalEnemyStatModel _normalEnemyStatModel;
	protected INormalEnemyView _view;
	protected INormalEnemyStateContext _stateContext;

	protected List<INormalEnemyTransition> _transitions = new List<INormalEnemyTransition>();

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
		foreach (var transition in _transitions)
		{
			if(transition.InConditionMet())
			{
				_stateContext.ChangeState(transition.TargetState, transition.TargetStateEnum);
			}
		}
	}


	public abstract void Exit();


	public abstract void SetupTransitions();
	
}
