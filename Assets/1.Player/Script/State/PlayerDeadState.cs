using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
	private readonly PlayerMoveController _moveController;
	
    public PlayerDeadState(PlayerMoveController moveController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
		_moveController = moveController;
    }


    public override void Enter()
    {
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    public override void SetupTransitions()
    {
        
    }
}
