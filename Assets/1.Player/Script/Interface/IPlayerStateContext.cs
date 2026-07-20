using UnityEngine;
public enum EPlayerState
{
	Idle = 0,
	Move = 1,
	Jump = 2,
	Attack = 3,
	Dash = 4,
	Hit = 5,
	Dead = 6
}
public interface IPlayerStateContext 
{
	EPlayerState CurrentStateEnum { get; }

	/// <summary>
	/// Player의 상태들
	/// </summary>
	public PlayerIdleState IdleState { get; }
	public PlayerMoveState MoveState { get; }
	public PlayerAttackState AttackState { get; }
	public PlayerDashState DashState { get; }
	public PlayerJumpState JumpState { get; }
	public PlayerHitState HitState { get; }
	public PlayerDeadState DeadState { get; }

	/// <summary>
	/// 상태 변경
	/// </summary>
	/// <param name="state">변경될 상태</param>
	/// <param name="stateEnum">변경될 상태에 맞춰 애니메이션을 컨트롤 하기 위한 Enum 값</param>
	public void ChangeState(IState state, EPlayerState stateEnum);
}
