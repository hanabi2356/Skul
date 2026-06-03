using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : IPlayerState
{
    protected PlayerBase playerBase;
    protected List<ITransition> transitions = new List<ITransition>();

    protected PlayerBaseState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }



    public abstract void Enter();


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


    public abstract void Exit();

    public abstract void SetupTransitions();
    
}
