using System;

public class BossEnemyTransition : IBossEnemyTransition
{
	private readonly Func<bool> _condition;

	public IState TargetState { get; private set; }
	public EBossEnemyState TargetStateEnum { get; private set; }

	public BossEnemyTransition(
		IState targetState,
		EBossEnemyState targetStateEnum,
		Func<bool> condition)
	{
		TargetState = targetState;
		TargetStateEnum = targetStateEnum;
		_condition = condition;
	}

	public bool InConditionMet()
	{
		return _condition != null && _condition.Invoke();
	}
}
