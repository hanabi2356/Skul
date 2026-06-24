using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

	private float _originGravityScale;
	public PlayerDashState(PlayerBase playerBase) : base(playerBase)
	{
		
	}


	public override void Enter()
	{
		_originGravityScale = playerBase.body.gravityScale;
		playerBase.body.gravityScale = 0.0f;
	}

	public override void Execute()
	{
		/*if(!playerBase.MoveController.isDashing && playerBase.PhysicsHandler.IsGround())
		{
			playerBase.ChangeState(playerBase.IdleState, EPlayerState.Idle);
		}
		if (!playerBase.MoveController.isDashing && !playerBase.PhysicsHandler.IsGround())
		{
			playerBase.ChangeState(playerBase.JumpState, EPlayerState.Jump);
		}*/

	   base.Execute();

	}

	public override void Exit()
	{
		playerBase.body.gravityScale= _originGravityScale;
	}

	public override void SetupTransitions()
	{
		transitions.Add(new Transition(playerBase.idleState, EPlayerState.Idle, () =>
			!playerBase.moveController.isDashing && playerBase.physicsHandler.IsGround()));

		transitions.Add(new Transition(playerBase.jumpState, EPlayerState.Jump, () =>
		!playerBase.moveController.isDashing && !playerBase.physicsHandler.IsGround()));
	}
}
