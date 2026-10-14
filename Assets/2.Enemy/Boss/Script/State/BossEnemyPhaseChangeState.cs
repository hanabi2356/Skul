using UnityEngine;

public class BossEnemyPhaseChangeState : BossEnemyBaseState
{
	private float _enterTime;
	private const float _phaseChangeDuration = 2.0f;

	public BossEnemyPhaseChangeState(IEnemyStatModel statModel, IBossEnemyView view, IBossEnemyStateContext stateContext) : base(statModel, view, stateContext)
	{
	}

	public override void Enter()
	{
		_enterTime = Time.time;
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
		_trasitions.Add(new BossEnemyTrasition(_stateContext.IdleState, EBossEnemyState.Idle,
		() => Time.time - _enterTime >= _phaseChangeDuration));

		_trasitions.Add(new BossEnemyTrasition(_stateContext.IdleState, EBossEnemyState.Idle,
			() => _view.UseSkill == false));
	}
}
