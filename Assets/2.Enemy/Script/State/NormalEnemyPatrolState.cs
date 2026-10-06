public class NormalEnemyPatrolState : NormalEnemyBaseState
{
	private readonly NormalEnemyMoveController _moveController;
	private readonly NormalEnemyRangeDetectionController _rangeController;
	private readonly NormalEnemyAttackController _attackController;

	public NormalEnemyPatrolState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyMoveController moveController,
		NormalEnemyRangeDetectionController rangeController,
		NormalEnemyAttackController attackController) : base(normalEnemyStatModel, view, stateContext)
	{
		_moveController = moveController;
		_rangeController = rangeController;
		_attackController = attackController;
	}

	public override void Enter()
	{
	}

	public override void Execute()
	{
		_moveController.MoveToPatrol();
		base.Execute();
	}

	public override void Exit()
	{
		_moveController.Stop();
	}

	public override void SetupTransitions()
	{
		_transitions.Add(new NormalEnemyTransition(_stateContext.DeadState, ENormalEnemyState.Dead, () =>
			_normalEnemyStatModel.IsDead));

		_transitions.Add(new NormalEnemyTransition(_stateContext.AttackState, ENormalEnemyState.Attack, () =>
			_rangeController.IsInAttackRange()
			&& _attackController.IsAttacking == false));

		_transitions.Add(new NormalEnemyTransition(_stateContext.TraceState, ENormalEnemyState.Trace, () =>
			_rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange()
			&& _rangeController.CanMove));
	}
}
