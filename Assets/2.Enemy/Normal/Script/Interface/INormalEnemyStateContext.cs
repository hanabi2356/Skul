using UnityEngine;

public enum ENormalEnemyState
{
	Idle = 0,
	Patrol = 1,
	Trace = 2,
	Attack = 3,
	Hit = 4,
	Dead = 5
}
public interface INormalEnemyStateContext : IStateContext<ENormalEnemyState>
{

	public NormalEnemyIdleState IdleState { get; }
	public NormalEnemyPatrolState PatrolState { get; }
	public NormalEnemyTraceState TraceState { get; }
	public NormalEnemyAttackState AttackState { get; }
	public NormalEnemyHitState HitState { get; }
	public NormalEnemyDeadState DeadState { get; }

	
}
