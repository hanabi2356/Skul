using UnityEngine;

public interface ITransition  
{
    public bool InConditionMet();
    public IPlayerState targteState { get; }
    public EPlayerState targetStateEnum { get; }
}
