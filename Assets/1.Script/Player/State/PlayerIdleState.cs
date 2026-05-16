using Mono.Cecil.Cil;
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
        if(playerBase.moveController.moveInput != Vector2.zero && playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.moveState, EPlayerState.Move);
        }
        if(!playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);    
        }
        if(playerBase.moveController.isDashing)
        {
            playerBase.ChangeState(playerBase.dashState, EPlayerState.Dash);
        }
        if(playerBase.attackController.attackCount>0 && !playerBase.attackController.isReset)
        {
            playerBase.ChangeState(playerBase.attackState, EPlayerState.Attack);
        }
    }

    public void Exit()
    {

    }
}
