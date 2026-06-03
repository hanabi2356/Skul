using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    
 
    public PlayerIdleState(PlayerBase playerBase) : base(playerBase)
    {

    }


    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        /*if(playerBase.moveController.moveInput != Vector2.zero && playerBase.physicsHandler.IsGround())
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
        }*/
        /*  foreach (var transition in transitions)
          {
              if (transition.InConditionMet())
              {
                  Debug.Log("InConditionMet Call");
                  playerBase.ChangeState(transition.targteState, transition.targetStateEnum);
                  return;
              }
          }*/
        base.Execute();
    }

    public override void Exit()
    {

    }

    public override void SetupTransitions()
    {
        transitions.Add(new Transition(playerBase.moveState, EPlayerState.Move, () =>
           playerBase.moveController.moveInput != Vector2.zero &&
           playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.jumpState, EPlayerState.Jump, () =>
            !playerBase.physicsHandler.IsGround()));

        transitions.Add(new Transition(playerBase.attackState, EPlayerState.Attack, () =>
            playerBase.attackController.attackCount > 0 &&
            !playerBase.attackController.isReset));
    }
}
