using System;
using UnityEngine;

public class NormalEnemyTransition : INormalEnemyTransition
{
	private Func<bool> _condition;
	public IState TargetState { get; }

	public ENormalState TargetStateEnum { get; }

	public NormalEnemyTransition(Func<bool> condition, 
		IState targetState, 
		ENormalState targetStateEnum)
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
