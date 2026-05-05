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
       
    }

    public void Excute()
    {
        if(playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }
    }

    public void Exit()
    {
    }

 
}
