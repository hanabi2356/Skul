public class BossEnemyDeadState : BossEnemyBaseState
{
	private readonly BossSkillController _skillController;

	public BossEnemyDeadState(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		IBossEnemyStateContext stateContext,
		BossSkillController skillController) : base(statModel, view, stateContext)
	{
		_skillController = skillController;
	}

	public override void Enter()
	{
		_skillController.Cancel();

		if (_view.Rigidbody != null)
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
