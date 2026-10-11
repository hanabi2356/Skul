public class BossEnemyIdleState : BossEnemyBaseState
{
	private readonly BossSkillController _skillController;

	public BossEnemyIdleState(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		IBossEnemyStateContext stateContext,
		BossSkillController skillController) : base(statModel, view, stateContext)
	{
		_skillController = skillController;
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
		_transitions.Add(new BossEnemyTransition(_stateContext.DeadState, EBossEnemyState.Dead, () =>
			_statModel.IsDead));

		_transitions.Add(new BossEnemyTransition(_stateContext.AttackState, EBossEnemyState.Attack, () =>
			_skillController.CanUseAnySkill));
	}
}
