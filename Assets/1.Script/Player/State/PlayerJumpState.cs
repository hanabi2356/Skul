using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public PlayerJumpState(PlayerBase playerBase) : base(playerBase)
    {

        
    }


    public override void Enter()
    {
       
    }

    public override void Execute()
    {
       
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

        transitions.Add(new Transition(playerBase.moveState, EPlayerState.Move,
            () => playerBase.physicsHandler.IsGround() && playerBase.moveController.moveInput != Vector2.zero));
    }
 
}
