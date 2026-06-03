using System;
using UnityEngine;

public class Transition : ITransition
{
    private Func<bool> condition;
    public IPlayerState targteState { get; }
    public EPlayerState targetStateEnum { get; }
    public Transition (IPlayerState targetState, EPlayerState targetStateEnum, Func<bool> condition)
    {
        this.targteState = targetState;
        this.targetStateEnum = targetStateEnum;
        this.condition = condition;
    }
    public bool InConditionMet()
    {
        return condition.Invoke();
    }

    
}
