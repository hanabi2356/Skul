using UnityEngine;
public enum EPlayerState
{
    Idle,
    Move,
    Attack,
    Dash,
    Hit,
    Dead
}

public interface IPlayerState 
{
    public void Enter();
    public void Excute();
    public void Exit();
}
