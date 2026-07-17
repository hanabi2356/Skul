using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

	private PlayerMoveController _moveController;
	public PlayerDashState(PlayerMoveController moveController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController = moveController;
	}


	public override void Enter()
	{
		_view.SetGravityScale(true);
	}

	public override void Execute()
	{
	   base.Execute();
	}

	public override void Exit()
	{
		_view.SetGravityScale(false);
	}

	public override void SetupTransitions()
	{
		transitions.Add(new Transition(_stateContext.IdleState, EPlayerState.Idle, () =>
			_moveController.IsDashing == false&& 
			_view.PhysicsHandler.IsGround()&&
			_moveController.MoveInput == Vector2.zero));

		transitions.Add(new Transition(_stateContext.JumpState, EPlayerState.Jump, () =>
		_moveController.IsDashing == false && 
		!_view.PhysicsHandler.IsGround()));

		transitions.Add(new Transition(_stateContext.MoveState, EPlayerState.Move, () =>
		_moveController.MoveInput.x != 0.0f &&
		_view.PhysicsHandler.IsGround()&&
		_moveController.IsDashing == false));
	}
}
