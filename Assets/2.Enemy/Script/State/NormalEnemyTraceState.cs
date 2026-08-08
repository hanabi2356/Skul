public class NormalEnemyTraceState : NormalEnemyBaseState
{
	private readonly NormalEnemyRangeDetectionController _rangeController;
	private readonly NormalEnemyMoveController _moveController;
	private readonly NormalEnemyAttackController _attackController;

	public NormalEnemyTraceState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyRangeDetectionController rangeController,
		NormalEnemyMoveController moveController,
		NormalEnemyAttackController attackController) : base(normalEnemyStatModel, view, stateContext)
	{
		_rangeController = rangeController;
		_moveController = moveController;
		_attackController = attackController;
	}

	public override void Enter()
	{
	}

	public override void Execute()
	{
		_moveController.MoveToTrace(_view.TargetPosition);
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

		_transitions.Add(new NormalEnemyTransition(_stateContext.PatrolState, ENormalEnemyState.Patrol, () =>
			_rangeController.IsInTraceRange() == false
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.IdleState, ENormalEnemyState.Idle, () =>
			_rangeController.IsInTraceRange() == false
			&& _rangeController.CanMove == false));
	}
}
