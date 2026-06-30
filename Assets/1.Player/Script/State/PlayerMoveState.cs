using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
	private PlayerMoveController _moveController;
	private PlayerAttackController _attackController;
	public PlayerMoveState(PlayerMoveController moveController,
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
		transitions.Add(new Transition(_stateContext.IdleState, EPlayerState.Idle,
			() => _moveController.MoveInput == Vector2.zero));

		transitions.Add(new Transition(_stateContext.JumpState, EPlayerState.Jump,
			() => !_view.PhysicsHandler.IsGround()));

		transitions.Add(new Transition(_stateContext.DashState, EPlayerState.Dash,
			() => _moveController.IsDashing));

		transitions.Add(new Transition(_stateContext.AttackState, EPlayerState.Attack,
			() => _attackController.IsAttacking == true && 
			!_attackController.IsReset));


	}
}
