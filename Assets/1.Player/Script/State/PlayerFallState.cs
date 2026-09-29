using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
	private readonly PlayerMoveController _moveController;

	public PlayerFallState(PlayerMoveController moveController, 
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController = moveController;
	}
	

	public override void Enter()
	{
	}

	public override void Exit()
	{
	}

	public override void SetupTransitions()
	{
		_transitions.Add(new PlayerTransition(_stateContext.IdleState, EPlayerState.Idle,
			() => _view.PhysicsHandler.IsGround()
			&& !_moveController.IsPassingOneWay
			&& _view.CurrentVelocityY <= 0.1f
			&& _moveController.MoveInput.x == 0.0f));

		_transitions.Add(new PlayerTransition(_stateContext.MoveState, EPlayerState.Move,
			() => _view.PhysicsHandler.IsGround()
			&& !_moveController.IsPassingOneWay
			&& _view.CurrentVelocityY <= 0.1f
			&& _moveController.MoveInput.x != 0.0f));

		_transitions.Add(new PlayerTransition(_stateContext.DashState, EPlayerState.Dash,
			() => _moveController.IsDashing == true));

		_transitions.Add(new PlayerTransition(_stateContext.HitState, EPlayerState.Hit, () =>
			_view.IsHit == true));
	}

}
