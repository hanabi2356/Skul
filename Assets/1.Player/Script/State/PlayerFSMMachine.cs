using UnityEngine;

public class PlayerFSMMachine : IFSMMachine, IPlayerStateContext
{
	public IState CurrentState { get; private set; }
	public EPlayerState CurrentStateEnum { get; private set; }


	public PlayerIdleState IdleState { get; private set; }

	public PlayerMoveState MoveState { get; private set; }

	public PlayerAttackState AttackState { get; private set; }

	public PlayerDashState DashState { get; private set; }

	public PlayerJumpState JumpState { get; private set; }

	public PlayerHitState HitState { get; private set; }

	public PlayerDeadState DeadState { get; private set; }

	public PlayerFSMMachine(PlayerMoveController moveController, 
		PlayerAttackController attackController,
		IPlayerView view, 
		IPlayerStatModel statModel)
	{
		InitState(moveController, attackController, view, statModel,this);
		SetupAllStateTrasitions();

		CurrentState = IdleState;
		CurrentStateEnum = EPlayerState.Idle;

		CurrentState.Enter();
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
		HitState = new PlayerHitState(moveController, view, statModel, stateContext);
		DeadState = new PlayerDeadState(moveController, view, statModel, stateContext);
	}
	public void ChangeState(IState state, EPlayerState stateEnum)
	{
		if (state == null || CurrentState == state) return;

		CurrentState.Exit();

		CurrentState = state;
		CurrentStateEnum = stateEnum;

		CurrentState.Enter();
	}
	private void SetupAllStateTrasitions()
	{
		TrySetup(IdleState);
		TrySetup(MoveState);
		TrySetup(AttackState);
		TrySetup(DashState);
		TrySetup(JumpState);
		TrySetup(HitState);
		TrySetup(DeadState);
	}
	private void TrySetup(IState state)
	{
		if(state is PlayerBaseState baseState)
		{
			baseState.SetupTransitions();
		}
	}
}
