using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IPlayerState
{
    protected PlayerBase playerBase;
    protected List<ITransition> transitions = new List<ITransition>();

    

    protected BaseState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }



    public abstract void Enter();

    public abstract void Execute();


    public abstract void Exit();
    
}
