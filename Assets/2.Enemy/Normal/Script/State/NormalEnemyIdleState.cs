public class NormalEnemyIdleState : NormalEnemyBaseState
{
	private readonly NormalEnemyRangeDetectionController _rangeController;
	private readonly NormalEnemyAttackController _attackController;
	public NormalEnemyIdleState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyRangeDetectionController rangeController,
		NormalEnemyAttackController attackController) : base(normalEnemyStatModel, view, stateContext)
	{
		_rangeController = rangeController;
		_attackController = attackController;
	}

	public override void Enter()
	{
	}

	public override void Execute()
	{
		base.Execute();
	}

	public override void Exit()
	{
	}

	public override void SetupTransitions()
	{
		_transitions.Add(new NormalEnemyTransition(_stateContext.DeadState, ENormalEnemyState.Dead, () =>
			_normalEnemyStatModel.IsDead));

		// 공격 사거리 안이면 즉시 공격 (정지형 적 포함)
		_transitions.Add(new NormalEnemyTransition(_stateContext.AttackState, ENormalEnemyState.Attack, () =>
			_rangeController.IsInAttackRange()
			&& _attackController.CanAttack));

		_transitions.Add(new NormalEnemyTransition(_stateContext.TraceState, ENormalEnemyState.Trace, () =>
			_rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange()
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.PatrolState, ENormalEnemyState.Patrol, () =>
			_rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange() == false
			&& _rangeController.CanMove));
	}
}
