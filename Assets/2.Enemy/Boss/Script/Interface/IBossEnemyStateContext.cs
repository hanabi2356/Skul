using UnityEngine;
public enum EBossEnemyState
{
	Idle=0,
	Attack=1,
	Hit=2,
	groggy=3,
	Dead =4,
	PhaseChange=5
}
public interface IBossEnemyStateContext : IStateContext<EBossEnemyState>
{
	public BossEnemyIdleState IdleState { get; }
	public BossEnemyAttackState AttackState { get; }
	public BossEnemyHitState HitState { get; }
	public BossEnemyGroggyState GroggyState { get; }
	public BossEnemyDeadState DeadState { get; }
	public BossEnemyPhaseChangeState PhaseChangeState { get; }
}
