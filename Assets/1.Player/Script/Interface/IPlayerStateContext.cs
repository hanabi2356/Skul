using UnityEngine;
public enum EPlayerState
{
	Idle = 0,
	Move = 1,
	Jump = 2,
	Attack = 3,
	Dash = 4,
	Hit = 5,
	Dead = 6,
	Fall = 7
}
public interface IPlayerStateContext : IStateContext<EPlayerState>
{
	/// <summary>
	/// Player의 상태들
	/// </summary>
	public PlayerIdleState IdleState { get; }
	public PlayerMoveState MoveState { get; }
	public PlayerAttackState AttackState { get; }
	public PlayerDashState DashState { get; }
	public PlayerJumpState JumpState { get; }
	public PlayerFallState FallState { get; }
	public PlayerHitState HitState { get; }
	public PlayerDeadState DeadState { get; }
}
