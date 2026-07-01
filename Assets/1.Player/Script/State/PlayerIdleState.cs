using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
	private PlayerMoveController _moveController;
	private PlayerAttackController _attackController;
    public PlayerIdleState(PlayerMoveController moveController,
		PlayerAttackController attackController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController = moveController;
		_attackController = attackController;
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
		
        transitions.Add(new Transition(_stateContext.MoveState, EPlayerState.Move, () =>
           _moveController.MoveInput != Vector2.zero &&
           _view.PhysicsHandler.IsGround()));

        transitions.Add(new Transition(_stateContext.JumpState, EPlayerState.Jump, () =>
            !_view.PhysicsHandler.IsGround()));

        transitions.Add(new Transition(_stateContext.AttackState, EPlayerState.Attack, () =>
            _attackController.IsAttacking == true &&
            !_attackController.IsReset));

		transitions.Add(new Transition(_stateContext.DashState, EPlayerState.Dash, () =>
		_moveController.IsDashing == true));
    }
}
