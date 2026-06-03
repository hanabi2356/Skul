using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

    private float originGravityScale;
    public PlayerDashState(PlayerBase playerBase) : base(playerBase)
    {
        
    }


    public override void Enter()
    {
        originGravityScale = playerBase.body.gravityScale;
        playerBase.body.gravityScale = 0.0f;
    }

    public override void Execute()
    {
        /*if(!playerBase.moveController.isDashing && playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }
        if (!playerBase.moveController.isDashing && !playerBase.physicsHandler.IsGround())
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
        }*/

       base.Execute();

    }

    public override void Exit()
    {
        playerBase.body.gravityScale= originGravityScale;
    }

    public override void SetupTransitions()
    {
        transitions.Add(new Transition(playerBase.idleState, EPlayerState.Idle, () =>
            !playerBase.moveController.isDashing && playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.jumpState, EPlayerState.Jump, () =>
        !playerBase.moveController.isDashing && !playerBase.physicsHandler.IsGround()));
    }
}
