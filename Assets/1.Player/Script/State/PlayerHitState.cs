using UnityEngine;

public class PlayerHitState : PlayerBaseState
{

    public PlayerHitState(PlayerMoveController moveController,
		IPlayerView view,
		IPlayerStatModel statModel,
		IPlayerStateContext stateContext) : base(view, statModel, stateContext)
	{
    }

    
    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void SetupTransitions()
    {
        
    }
}
