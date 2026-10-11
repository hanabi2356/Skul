using UnityEngine;

public interface IBossEnemyTransition
{
	public IState TargetState { get; }
	public EBossEnemyState TargetStateEnum { get; }
	public bool InConditionMet();
}
