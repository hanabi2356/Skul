using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    private PlayerBase playerBase;
    public PlayerJumpState(PlayerBase playerBase)
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
