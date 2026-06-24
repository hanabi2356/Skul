using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{

	public PlayerAttackState( PlayerBase playerBase ) : base( playerBase )
	{

	}


	public override void Enter()
	{
		
	}

	public override void Execute()
	{
		/*if (playerBase.MoveController.moveInput == Vector2.zero && playerBase.AttackController.attackCount==0
			&& playerBase.PhysicsHandler.IsGround() )
		{
			playerBase.ChangeState(playerBase.IdleState, EPlayerState.Idle);
		}

		if (!playerBase.PhysicsHandler.IsGround() && playerBase.AttackController.attackCount == 0)
		{
			playerBase.ChangeState(playerBase.JumpState, EPlayerState.Jump);
		}

		if (playerBase.MoveController.moveInput != Vector2.zero && playerBase.PhysicsHandler.IsGround()
			&& playerBase.AttackController.attackCount == 0)
		{
			playerBase.ChangeState(playerBase.MoveState, EPlayerState.Move);
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
}
