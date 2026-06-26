using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
	private PlayerMoveController _moveController;
	private PlayerAttackController _attackController;
	public PlayerAttackState(PlayerMoveController moveController, 
		PlayerAttackController attackController,
		IPlayerView view, 
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController = moveController;
		_attackController = attackController;
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

		transitions.Add(new Transition(_stateContext.MoveState, EPlayerState.Move, () =>
			_moveController.MoveInput != Vector2.zero &&
			_view.PhysicsHandler.IsGround() &&
			_attackController.AttackCount == 0));

		transitions.Add(new Transition(_stateContext.IdleState, EPlayerState.Idle, () =>
			_moveController.MoveInput == Vector2.zero &&
			_attackController.AttackCount == 0 &&
			_view.PhysicsHandler.IsGround()));

		transitions.Add(new Transition(_stateContext.JumpState, EPlayerState.Jump, () =>
			!_view.PhysicsHandler.IsGround() &&
			_attackController.AttackCount == 0));
	}
}
