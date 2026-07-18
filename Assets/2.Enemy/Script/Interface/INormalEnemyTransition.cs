using UnityEngine;

public interface INormalEnemyTransition
{
	public bool InConditionMet();

	public IState TargetState { get; }
	public ENormalState TargetStateEnum { get; }
}
