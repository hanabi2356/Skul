public class BossEnemyAttackState : BossEnemyBaseState
{
	private readonly BossSkillController _skillController;

	public BossEnemyAttackState(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		IBossEnemyStateContext stateContext,
		BossSkillController skillController) : base(statModel, view, stateContext)
	{
		_skillController = skillController;
	}

	public override void Enter()
	{
		_skillController.TryBeginSkill();
	}

	public override void Execute()
	{
		_skillController.Tick();
		base.Execute();
	}

	public override void Exit()
	{
		if (_skillController.IsUsingSkill)
		{
			_skillController.Cancel();
		}
	}

	public override void SetupTransitions()
	{
		_transitions.Add(new BossEnemyTransition(_stateContext.DeadState, EBossEnemyState.Dead, () =>
			_statModel.IsDead));

		_transitions.Add(new BossEnemyTransition(_stateContext.IdleState, EBossEnemyState.Idle, () =>
			_skillController.IsSkillCompleted
			|| (_skillController.IsUsingSkill == false && _skillController.IsOnCoolDown)
			|| (_skillController.IsUsingSkill == false && _skillController.HasSelectedSkill == false)));
	}
}
