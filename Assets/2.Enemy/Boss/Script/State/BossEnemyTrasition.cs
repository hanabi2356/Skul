using System;
using UnityEngine;

public class BossEnemyTrasition : IBossEnemyTransition
{
	private Func<bool> _condition;
	public IState TargetState { get; private set; }

	public EBossEnemyState TargetStateEnum { get; private set; }

	public BossEnemyTrasition(IState targetState, EBossEnemyState targetStateEnum, Func<bool> condition)
	{
		TargetState = targetState;
		TargetStateEnum = targetStateEnum;
		_condition = condition;
	}
	public bool InConditionMet()
	{
		return _condition.Invoke();
	}

	
}
