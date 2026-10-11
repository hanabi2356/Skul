using System.Collections.Generic;

public abstract class BossEnemyBaseState : IState
{
	protected IEnemyStatModel _statModel;
	protected IBossEnemyView _view;
	protected IBossEnemyStateContext _stateContext;
	protected List<IBossEnemyTransition> _transitions = new List<IBossEnemyTransition>();

	public BossEnemyBaseState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext)
	{
		_statModel = statModel;
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
