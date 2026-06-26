using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerBase playerBase;
	protected IPlayerView _view;
	protected IPlayerStatModel _statModel;
	protected IPlayerStateContext _stateContext;
    //전이 조건을 담는 List
    protected List<ITransition> transitions = new List<ITransition>();


    protected PlayerBaseState(IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext context)
    {
		_view = view;
		_statModel = statModel;
		_stateContext = context;
    }



    public abstract void Enter();


    public virtual void Execute()
    {
        //transitions List를 순회 하면서 상태 변경 조건이 충족 되었는지 확인 하고 맞다면 변경
        foreach (var transition in transitions)
        {
            if (transition.InConditionMet())
            {
                _stateContext.ChangeState(transition.targteState, transition.targetStateEnum);
                return;
            }
        }
    }


    public abstract void Exit();

    public abstract void SetupTransitions();
    
}
