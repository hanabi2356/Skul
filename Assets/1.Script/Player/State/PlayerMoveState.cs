using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerBase playerBase;
    public PlayerMoveState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        
    }

    public void Excute()
    {
        if(playerBase.moveController.moveInput == Vector2.zero)
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }

        if(!playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
        }
    }

    public void Exit()
    {
        
    }

   
}
