using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    private PlayerBase playerBase;
    public PlayerAttackState( PlayerBase playerBase )
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        
    }

    public void Excute()
    {
        if (playerBase.moveController.moveInput == Vector2.zero && !playerBase.animController.isAttackAnimPlaying
            && playerBase.physicsHandler.IsGround() )
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }

        if (!playerBase.physicsHandler.IsGround() && !playerBase.animController.isAttackAnimPlaying)
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
        }

        if (playerBase.moveController.moveInput != Vector2.zero && playerBase.physicsHandler.IsGround()
            && !playerBase.animController.isAttackAnimPlaying)
        {
            playerBase.ChangeState(playerBase.moveState, EPlayerState.Move);
        }
    }

    public void Exit()
    {
        
    }

    
}
