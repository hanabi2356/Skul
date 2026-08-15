using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHitState : PlayerBaseState
{
	private readonly PlayerMoveController _moveController;
	private readonly PlayerAttackController _attackController;
	private float _enterTime;
	private const float _hitDuration = 0.3f;
    public PlayerHitState(PlayerMoveController moveController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext,
		PlayerAttackController attackController) : base(view, statModel, stateContext)
	{
		_attackController = attackController;
		_moveController = moveController;
	}


	public override void Enter()
    {
		_enterTime = Time.time;
		_moveController.MoveStop();
		_attackController.ResetCombo();
		_view.SetIsHit(true);

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
		_transitions.Add(new PlayerTransition(_stateContext.DeadState, EPlayerState.Dead, ()=>
		_statModel.CurrentHP <= 0));

		_transitions.Add(new PlayerTransition(_stateContext.IdleState, EPlayerState.Idle, () =>
		Time.time - _enterTime >= _hitDuration));
	}

	
}
