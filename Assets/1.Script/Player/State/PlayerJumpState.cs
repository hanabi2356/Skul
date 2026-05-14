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
        if (playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }

        if (playerBase.moveController.isDashing)
        {
            playerBase.ChangeState(playerBase.dashState, EPlayerState.Dash);
        }

        if (playerBase.attackController.attackCount > 0)
        {
            playerBase.ChangeState(playerBase.attackState, EPlayerState.Attack);
        }
    }

    public void Exit()
    {
    }

 
}
