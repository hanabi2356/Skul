using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private PlayerBase playerBase;
    
    
    public PlayerIdleState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
    }

    public void Excute()
    {
        if(playerBase.moveController.moveInput != Vector2.zero)
        {
            playerBase.ChangeState(playerBase.moveState, EPlayerState.Move);
        }
    }

    public void Exit()
    {

    }
}
