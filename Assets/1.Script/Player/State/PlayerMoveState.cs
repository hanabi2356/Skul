using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : BaseState
{

    public PlayerMoveState(PlayerBase playerBase) : base(playerBase)
    {
        this.playerBase = playerBase;


        transitions.Add(new Transition(playerBase.idleState, EPlayerState.Idle,
            () => playerBase.moveController.moveInput == Vector2.zero));

        transitions.Add(new Transition(playerBase.jumpState, EPlayerState.Jump,
            () => !playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.dashState, EPlayerState.Dash,
            () => playerBase.moveController.isDashing));

        transitions.Add(new Transition(playerBase.attackState, EPlayerState.Attack,
            () => playerBase.attackController.attackCount > 0 && !playerBase.attackController.isReset));



    }


    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        /* if(playerBase.moveController.moveInput == Vector2.zero)
         {
             playerBase.ChangeState(playerBase.idleState, EPlayerState.Idle);
         }

         if(!playerBase.physicsHandler.IsGround())
         {
             playerBase.ChangeState(playerBase.jumpState, EPlayerState.Jump);
         }

         if (playerBase.moveController.isDashing)
         {
             playerBase.ChangeState(playerBase.dashState, EPlayerState.Dash);
         }

         if (playerBase.attackController.attackCount > 0 && !playerBase.attackController.isReset)
         {
             playerBase.ChangeState(playerBase.attackState, EPlayerState.Attack);
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
