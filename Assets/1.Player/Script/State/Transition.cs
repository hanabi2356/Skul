using System;
using UnityEngine;

public class Transition : ITransition
{
    //bool 값을 반환하는 delegate
    private Func<bool> condition;
    public IState targteState { get; }
    public EPlayerState targetStateEnum { get; }

    /// <summary>
    /// IPlayerStatem EPlayerState 의존성 주입
    /// </summary>
    /// <param name="targetState">변환할 상태</param>
    /// <param name="targetStateEnum">변환할 상태에 대한 Enum</param>
    /// <param name="condition">전이 조건</param>
    public Transition (IState targetState, EPlayerState targetStateEnum, Func<bool> condition)
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
