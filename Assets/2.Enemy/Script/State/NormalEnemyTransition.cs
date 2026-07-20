using System;
using UnityEngine;

public class NormalEnemyTransition : INormalEnemyTransition
{
	private Func<bool> _condition;
	public IState TargetState { get; private set; }

	public ENormalEnemyState TargetStateEnum { get; private set; }

	public NormalEnemyTransition(Func<bool> condition, 
		IState targetState, 
		ENormalEnemyState targetStateEnum)
	{
		_condition = condition;
		TargetState = targetState;
		TargetStateEnum = targetStateEnum;
	}

	public bool InConditionMet()
	{
		return _condition.Invoke();
	}
}
