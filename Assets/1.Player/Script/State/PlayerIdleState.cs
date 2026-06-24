using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    
    public PlayerIdleState(PlayerBase playerBase) : base(playerBase)
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
