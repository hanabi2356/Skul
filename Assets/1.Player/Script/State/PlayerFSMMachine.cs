using UnityEngine;

public class PlayerFSMMachine : FSMMachine<EPlayerState>, IPlayerStateContext
{

	public PlayerIdleState IdleState { get; private set; }

	public PlayerMoveState MoveState { get; private set; }

	public PlayerAttackState AttackState { get; private set; }

	public PlayerDashState DashState { get; private set; }

	public PlayerJumpState JumpState { get; private set; }

	public PlayerHitState HitState { get; private set; }

	public PlayerDeadState DeadState { get; private set; }

	public PlayerFallState FallState {get; private set; }

	public PlayerFSMMachine(PlayerMoveController moveController, 
		PlayerAttackController attackController,
		IPlayerView view, 
		IPlayerStatModel statModel)
	{
		InitState(moveController, attackController, view, statModel,this);
		
	}
	public void BootUp()
	{
		base.BootUp(IdleState, EPlayerState.Idle,
			IdleState, MoveState, AttackState, DashState,
			JumpState, FallState, HitState, DeadState);
	}

	private void InitState(PlayerMoveController moveController,
		PlayerAttackController attackController,
		IPlayerView view, 
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext)
	{
		IdleState = new PlayerIdleState(moveController,attackController, view, statModel, stateContext);
		MoveState = new PlayerMoveState(moveController, attackController, view, statModel, stateContext);
		AttackState = new PlayerAttackState(moveController, attackController, view, statModel, stateContext);
		DashState = new PlayerDashState(moveController, view, statModel, stateContext);
		JumpState = new PlayerJumpState(moveController, attackController, view, statModel, stateContext);
		FallState = new PlayerFallState(moveController, view, statModel,stateContext);
		HitState = new PlayerHitState(moveController, view, statModel, stateContext, attackController);
		DeadState = new PlayerDeadState(moveController, view, statModel, stateContext);
	}
	
	
}
