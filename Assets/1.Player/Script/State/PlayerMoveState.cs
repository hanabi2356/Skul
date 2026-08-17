using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
	private readonly PlayerMoveController _moveController;
	private readonly PlayerAttackController _attackController;
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
		_transitions.Add(new PlayerTransition(_stateContext.IdleState, EPlayerState.Idle,
			() => _moveController.MoveInput.x == 0.0f));

		_transitions.Add(new PlayerTransition(_stateContext.JumpState, EPlayerState.Jump,
			() => !_view.PhysicsHandler.IsGround()));

		_transitions.Add(new PlayerTransition(_stateContext.DashState, EPlayerState.Dash,
			() => _moveController.IsDashing == true));

		_transitions.Add(new PlayerTransition(_stateContext.AttackState, EPlayerState.Attack,
			() => _attackController.IsAttacking == true && 
			!_attackController.IsReset));

		_transitions.Add(new PlayerTransition(_stateContext.HitState, EPlayerState.Hit, () =>
		_view.IsHit == true));

	}
}
