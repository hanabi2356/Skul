using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{

    public PlayerAttackState( PlayerBase playerBase ) : base( playerBase )
    {

        transitions.Add(new Transition(playerBase.moveState, EPlayerState.Move, () =>
            playerBase.moveController.moveInput != Vector2.zero &&
            playerBase.physicsHandler.IsGround() &&
            playerBase.attackController.attackCount == 0));

        transitions.Add(new Transition(playerBase.idleState, EPlayerState.Idle, () =>
            playerBase.moveController.moveInput == Vector2.zero &&
            playerBase.attackController.attackCount == 0 &&
            playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.jumpState, EPlayerState.Jump, () =>
            !playerBase.physicsHandler.IsGround() &&
            playerBase.attackController.attackCount == 0));
    }


    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        /*if (playerBase.moveController.moveInput == Vector2.zero && playerBase.attackController.attackCount==0
            && playerBase.physicsHandler.IsGround() )
        {
            playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
        }

        if (!playerBase.physicsHandler.IsGround() && playerBase.attackController.attackCount == 0)
        {
            playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
        }

        if (playerBase.moveController.moveInput != Vector2.zero && playerBase.physicsHandler.IsGround()
            && playerBase.attackController.attackCount == 0)
        {
            playerBase.ChangeState(playerBase.moveState, EPlayerState.Move);
        }*/

        foreach (var transition in transitions)
        {
            if (transition.InConditionMet())
            {
                playerBase.ChangeState(transition.targteState, transition.targetStateEnum);
            }
        }
    }

    public override void Exit()
    {
        
    }

    
}
