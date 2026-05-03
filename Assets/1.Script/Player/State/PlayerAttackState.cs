using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    private PlayerBase playerBase;
    public PlayerAttackState( PlayerBase playerBase )
    {
        this.playerBase = playerBase;
    }
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Excute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    
}
