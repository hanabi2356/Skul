using System.Collections.Generic;
using UnityEngine;

public class BaseState : IPlayerState
{
    protected PlayerBase playerBase;
    protected List<ITransition> transitions = new List<ITransition>();

    protected BaseState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }



    public virtual void Enter() { }


    public virtual void Execute()
    {
        foreach (var transition in transitions)
        {
            if (transition.InConditionMet())
            {
                playerBase.ChangeState(transition.targteState, transition.targetStateEnum);
                return;
            }
        }
    }
    

    public virtual void Exit() { }

    public virtual void SetupTransitions() { }
    
}
