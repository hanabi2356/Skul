using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    private PlayerBase playerBase;
    public PlayerDeadState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Excute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
