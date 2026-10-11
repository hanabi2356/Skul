using UnityEngine;

public class BossEnemyHitState : BossEnemyBaseState
{
	private readonly BossSkillController _skillController;
	private float _enterTime;
	private const float HitDuration = 0.3f;

	public BossEnemyHitState(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		IBossEnemyStateContext stateContext,
		BossSkillController skillController) : base(statModel, view, stateContext)
	{
		_skillController = skillController;
	}

	public override void Enter()
	{
		_enterTime = Time.time;
		_skillController.Cancel();
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
			IsHitFinished() && _skillController.CanUseAnySkill));

		_transitions.Add(new BossEnemyTransition(_stateContext.IdleState, EBossEnemyState.Idle, () =>
			IsHitFinished()));
	}

	private bool IsHitFinished() => Time.time - _enterTime >= HitDuration;
}
