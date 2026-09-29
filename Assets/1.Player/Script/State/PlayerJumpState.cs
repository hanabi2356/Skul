using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
	private readonly PlayerMoveController _moveController;
	private readonly PlayerAttackController _attackController;
	
    public PlayerJumpState(PlayerMoveController moveController,
		PlayerAttackController attackController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController	= moveController;
        _attackController = attackController;
    }


    public override void Enter()
    {
       if(_stateContext.CurrentStateEnum == EPlayerState.Dash)
		{
			_moveController.SetIsDashing(false);
		}
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
		// 상승 중 OneWay와 겹쳐 IsGround가 잠깐 true여도 Idle로 안 가게 vy 가드
        _transitions.Add(new PlayerTransition(_stateContext.IdleState, EPlayerState.Idle,
            () => _view.PhysicsHandler.IsGround()
			&& _moveController.IsPassingOneWay == false
			&& _view.CurrentVelocityY <= 0.1f));

        _transitions.Add(new PlayerTransition(_stateContext.DashState, EPlayerState.Dash,
            () => _moveController.IsDashing == true));

        _transitions.Add(new PlayerTransition(_stateContext.AttackState, EPlayerState.Attack,
            () => _attackController.IsAttacking == true && 
			!_attackController.IsReset));

        _transitions.Add(new PlayerTransition(_stateContext.MoveState, EPlayerState.Move,
            () => _view.PhysicsHandler.IsGround()
			&& _moveController.IsPassingOneWay == false
			&& _moveController.MoveInput.x != 0.0f
			&& _view.CurrentVelocityY <= 0.1f));

		_transitions.Add(new PlayerTransition(_stateContext.FallState, EPlayerState.Fall, 
			() => !_view.PhysicsHandler.IsGround()
			&& _view.CurrentVelocityY <= 0.0f));

		_transitions.Add(new PlayerTransition(_stateContext.HitState, EPlayerState.Hit, () =>
		_view.IsHit == true));

	}
 
}
