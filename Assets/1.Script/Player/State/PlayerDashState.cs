using UnityEngine;

public class PlayerDashState : IPlayerState
{
    private PlayerBase playerBase;
    private float originGravityScale;
    public PlayerDashState(PlayerBase playerBase)
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        originGravityScale = playerBase.body.gravityScale;
        playerBase.body.gravityScale = 0.0f;
    }

    public void Excute()
    {
        if(!playerBase.moveController.isDashing && playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }
        if (!playerBase.moveController.isDashing && !playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
        }

    }

    public void Exit()
    {
        playerBase.body.gravityScale= originGravityScale;
    }
}
