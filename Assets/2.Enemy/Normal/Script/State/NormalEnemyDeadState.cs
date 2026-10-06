public class NormalEnemyDeadState : NormalEnemyBaseState
{
	private readonly NormalEnemyMoveController _moveController;
	private readonly NormalEnemyAttackController _attackController;

	public NormalEnemyDeadState(INormalEnemyStatModel normalEnemyStatModel,
		INormalEnemyView view,
		INormalEnemyStateContext stateContext,
		NormalEnemyMoveController moveController,
		NormalEnemyAttackController attackController) : base(normalEnemyStatModel, view, stateContext)
	{
		_moveController = moveController;
		_attackController = attackController;
	}

	public override void Enter()
	{
		_moveController.Stop();
		_attackController.CancelAttack();

		if(_view.Rigidbody != null)
		{
			_view.Rigidbody.simulated = false;
		}
	}

	public override void Execute()
	{
		// Dead에서는 전이하지 않음
	}

	public override void Exit()
	{
	}

	public override void SetupTransitions()
	{
	}
}
