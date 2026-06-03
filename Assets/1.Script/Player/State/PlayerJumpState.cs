using UnityEngine;

public class PlayerJumpState : BaseState
{

    public PlayerJumpState(PlayerBase playerBase) : base(playerBase)
    {

        
    }


    public override void Enter()
    {
       
    }

    public override void Execute()
    {
        /*if (playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }

        if (playerBase.moveController.isDashing)
        {
            playerBase.ChangeState(playerBase.dashState, EPlayerState.Dash);
        }

        if (playerBase.attackController.attackCount > 0 && !playerBase.attackController.isReset)
        {
            playerBase.ChangeState(playerBase.attackState, EPlayerState.Attack);
        }*/
        base.Execute();
    }

    public override void Exit()
    {
    }

    public override void SetupTransitions()
    {
        transitions.Add(new Transition(playerBase.idleState, EPlayerState.Idle,
            () => playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.dashState, EPlayerState.Dash,
            () => playerBase.moveController.isDashing));

        transitions.Add(new Transition(playerBase.attackState, EPlayerState.Attack,
            () => playerBase.attackController.attackCount > 0
            && !playerBase.attackController.isReset));
    }
 
}
