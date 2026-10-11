using System.Collections.Generic;


public abstract class BossEnemyBaseState : IState
{
	protected IEnemyStatModel _statModel;
	protected IBossEnemyView _view;
	protected IBossEnemyStateContext _stateContext;
	protected List<IBossEnemyTransition> _trasitions=new List<IBossEnemyTransition>();

	public BossEnemyBaseState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext)
	{
		_statModel = statModel;
		_view = view;
		_stateContext = stateContext;
	}

	public abstract void Enter();
	
	public virtual void Execute()
	{
		foreach(var trasition in _trasitions)
		{
			if(trasition.InConditionMet())
			{
				_stateContext.ChangeState(trasition.TargetState, trasition.TargetStateEnum);
				return;
			}
		}
	}

	public abstract void Exit();
	public abstract void SetupTransitions();

	
}
