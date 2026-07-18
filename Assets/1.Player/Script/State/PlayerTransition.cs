using System;
using UnityEngine;

public class PlayerTransition : IPlayerTransition
{
    //bool 값을 반환하는 delegate
    private Func<bool> _condition;
    public IState TargteState { get; }
    public EPlayerState TargetStateEnum { get; }

    /// <summary>
    /// IPlayerStatem EPlayerState 의존성 주입
    /// </summary>
    /// <param name="targetState">변환할 상태</param>
    /// <param name="targetStateEnum">변환할 상태에 대한 Enum</param>
    /// <param name="condition">전이 조건</param>
    public PlayerTransition (IState targetState, EPlayerState targetStateEnum, Func<bool> condition)
    {
        this.TargteState = targetState;
        this.TargetStateEnum = targetStateEnum;
        this._condition = condition;
    }
    public bool InConditionMet()
    {
        return _condition.Invoke();
    }

    
}
