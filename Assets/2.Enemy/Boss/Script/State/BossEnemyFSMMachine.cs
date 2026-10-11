public class BossEnemyFSMMachine : FSMMachine<EBossEnemyState>, IBossEnemyStateContext
{
	public BossEnemyIdleState IdleState { get; private set; }
	public BossEnemyAttackState AttackState { get; private set; }
	public BossEnemyGroggyState GroggyState { get; private set; }
	public BossEnemyHitState HitState { get; private set; }
	public BossEnemyDeadState DeadState { get; private set; }
	public BossEnemyPhaseChangeState PhaseChangeState { get; private set; }

	public BossEnemyFSMMachine(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		BossSkillController skillController)
	{
		InitState(statModel, view, this, skillController);
	}

	public void BootUp()
	{
		base.BootUp(
			IdleState,
			EBossEnemyState.Idle,
			IdleState,
			AttackState,
			GroggyState,
			HitState,
			DeadState,
			PhaseChangeState);
	}

	private void InitState(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		IBossEnemyStateContext stateContext,
		BossSkillController skillController)
	{
		IdleState = new BossEnemyIdleState(statModel, view, stateContext, skillController);
		AttackState = new BossEnemyAttackState(statModel, view, stateContext, skillController);
		GroggyState = new BossEnemyGroggyState(statModel, view, stateContext);
		HitState = new BossEnemyHitState(statModel, view, stateContext, skillController);
		DeadState = new BossEnemyDeadState(statModel, view, stateContext, skillController);
		PhaseChangeState = new BossEnemyPhaseChangeState(statModel, view, stateContext);
	}
}
