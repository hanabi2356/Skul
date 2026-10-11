using System.Collections.Generic;

public abstract class NormalEnemyBaseState : IState
{
	protected IEnemyStatModel _enemyStatModel;
	protected INormalEnemyView _view;
	protected INormalEnemyStateContext _stateContext;
	protected List<INormalEnemyTransition> _transitions = new List<INormalEnemyTransition>();

	protected NormalEnemyBaseState(IEnemyStatModel enemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext)
	{
		_enemyStatModel = enemyStatModel;
		_view = view;
		_stateContext = stateContext;
	}

	public abstract void Enter();

	public virtual void Execute()
	{
		foreach (var transition in _transitions)
		{
			if (transition.InConditionMet())
			{
				_stateContext.ChangeState(transition.TargetState, transition.TargetStateEnum);
				return;
			}
		}
	}

	public abstract void Exit();
	public abstract void SetupTransitions();
}
