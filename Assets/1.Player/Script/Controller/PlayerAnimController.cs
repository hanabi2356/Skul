using UnityEngine;

public class PlayerAnimController 
{
	private IPlayerView _view;
    
    public bool IsAttackAnimPlaying { get; private set; } = false;
    
	public PlayerAnimController(IPlayerView view)
	{
		_view = view;
	}

    public void ChangeAnim(EPlayerState state, int attackCount)
    {
		_view.Animator.SetInteger("State", ((int)state));
		_view.Animator.SetInteger("AttackCount", attackCount);
	}

	public void AttackAnimUpdate()
    {
        IsAttackAnimPlaying = true;
    }

    public void AttackAnimEnd()
    {
        IsAttackAnimPlaying = false;
    }

    private void OnDisable()
    {
        /*if( playerBase != null )
            playerBase.attackController.onAttackStarted -= AttackAnimUpdate;*/
    }

}
