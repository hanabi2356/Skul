using UnityEngine;

public class NormalEnemyHitState : NormalEnemyBaseState
{
	private readonly NormalEnemyMoveController _moveController;
	private readonly NormalEnemyAttackController _attackController;
	private readonly NormalEnemyRangeDetectionController _rangeController;
	private float _enterTime;
	private const float HitDuration = 0.3f;

	public NormalEnemyHitState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyMoveController moveController,
		NormalEnemyAttackController attackController,
		NormalEnemyRangeDetectionController rangeController) : base(normalEnemyStatModel, view, stateContext)
	{
		_moveController = moveController;
		_attackController = attackController;
		_rangeController = rangeController;
	}

	public override void Enter()
	{
		_enterTime = Time.time;
		_moveController.Stop();
		_attackController.CancelAttack();
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

		_transitions.Add(new NormalEnemyTransition(_stateContext.AttackState, ENormalEnemyState.Attack, () =>
			IsHitFinished()
			&& _rangeController.IsInAttackRange()));

		_transitions.Add(new NormalEnemyTransition(_stateContext.TraceState, ENormalEnemyState.Trace, () =>
			IsHitFinished()
			&& _rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange()
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.PatrolState, ENormalEnemyState.Patrol, () =>
			IsHitFinished()
			&& _rangeController.IsInAttackRange() == false
			&& _rangeController.IsInTraceRange() == false
			&& _rangeController.CanMove));

		_transitions.Add(new NormalEnemyTransition(_stateContext.IdleState, ENormalEnemyState.Idle, () =>
			IsHitFinished()));
	}

	private bool IsHitFinished() => Time.time - _enterTime >= HitDuration;
}
