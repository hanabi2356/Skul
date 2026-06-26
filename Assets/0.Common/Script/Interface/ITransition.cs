using UnityEngine;

public interface ITransition  
{
    /// <summary>
    /// 조건이 맞을 때 Delegate를 Invoke한다
    /// </summary>
    /// <returns>true: 전이, false: 현 상태 유지</returns>
    public bool InConditionMet();
    /// <summary>
    /// 전환할 상태
    /// </summary>
    public IState TargteState { get; }
    /// <summary>
    /// 전환할 상태에 맞춘 Enum
    /// Enum을 사용하는 이유는 Animation 모션을 상태와 맞추기 위해 사용함
    /// </summary>
    public EPlayerState TargetStateEnum { get; }
}
