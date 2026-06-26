using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

	private float _originGravityScale;
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
		_originGravityScale = _view.Body.gravityScale;
		_view.Body.gravityScale = 0.0f;
	}

	public override void Execute()
	{
	   base.Execute();
	}

	public override void Exit()
	{
		_view.Body.gravityScale= _originGravityScale;
	}

	public override void SetupTransitions()
	{
		transitions.Add(new Transition(_stateContext.IdleState, EPlayerState.Idle, () =>
			!_moveController.IsDashing && 
			_view.PhysicsHandler.IsGround()));

		transitions.Add(new Transition(_stateContext.JumpState, EPlayerState.Jump, () =>
		!_moveController.IsDashing && 
		!_view.PhysicsHandler.IsGround()));
	}
}
