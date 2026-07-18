using UnityEngine;



public interface INormalEnemyStateContext
{
    public ENormalState CurrentStateEnum { get; }

	public NormalEnemyIdleState IdleState { get; }
	public NormalEnemyPatrolState PatrolState { get; }
	public NormalEnemyTraceState TraceState { get; }
	public NormalEnemyAttackState AttackState { get; }
	public NormalEnemyHitState HitState { get; }
	public NormalEnemyDeadState DeadState { get; }

	/// <summary>
	/// 상태 변경
	/// </summary>
	/// <param name="state">변경할 상태</param>
	/// <param name="stateEnum">변경될 상태에 맞춰 애니메이션을 컨트롤 하기 위한 Enum 값</param>
	public void ChangeState(IState state, ENormalState stateEnum);
}
