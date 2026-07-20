using UnityEngine;

public interface INormalEnemyTransition
{

	public IState TargetState { get; }
	public ENormalEnemyState TargetStateEnum { get; }
	public bool InConditionMet();
}
