public class NormalEnemyAttackState : NormalEnemyBaseState
{
	private readonly NormalEnemyRangeDetectionController _rangeController;
	private readonly NormalEnemyMoveController _moveController;
	private readonly NormalEnemyAttackController _attackController;

	public NormalEnemyAttackState(INormalEnemyStatModel normalEnemyStatModel,
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
		_moveController.Stop();
		_moveController.FaceTarget(_view.TargetPosition);
		_attackController.TryAttack();
	}

	public override void Execute()
	{
		_moveController.FaceTarget(_view.TargetPosition);
		_attackController.Tick();

		if (_attackController.IsAttacking == false && _attackController.CanAttack
			&& _rangeController.IsInAttackRange())
		{
			_attackController.TryAttack();
		}

		base.Execute();
	}

	public override void Exit()
	{
		_attackController.CancelAttack();
	}

	public override void SetupTransitions()
	{
		_transitions.Add(new NormalEnemyTransition(_stateContext.DeadState, ENormalEnemyState.Dead, () =>
			_normalEnemyStatModel.IsDead));

		_transitions.Add(new NormalEnemyTransition(_stateContext.IdleState, ENormalEnemyState.Idle, () =>
		_attackController.IsAttacking == false
		&& _attackController.IsOnCoolDown
		&& _rangeController.IsInAttackRange()));
		
		_transitions.Add(new NormalEnemyTransition(_stateContext.TraceState, ENormalEnemyState.Trace, () =>
			_attackController.IsAttacking == false
			&& _rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange()
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.PatrolState, ENormalEnemyState.Patrol, () =>
			_attackController.IsAttacking == false
			&& _rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange() == false
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.IdleState, ENormalEnemyState.Idle, () =>
			_attackController.IsAttacking == false
			&& _rangeController.IsInAttackRange() == false
			&& _rangeController.CanMove == false));
	}
}
