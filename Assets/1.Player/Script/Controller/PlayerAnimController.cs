using UnityEngine;

public class PlayerAnimController 
{
	private IPlayerView _view;
    
    public bool isAttackAnimPlaying { get; private set; } = false;
    void Awake()
    {
       
    }

    private void Start()
    {
/*        if (playerBase != null)
        {
            playerBase.attackController.onAttackStarted += AttackAnimUpdate;
            playerBase.attackController.onAttackFinished += AttackAnimEnd;
        }*/


    }

    void Update()
    {
        //ChangeAnim(playerBase.currentPlayerStateEnum);
        
    }
    private void ChangeAnim(EPlayerState state)
    {
        //playerBase.animator.SetInteger("State", ((int)state));
    }
    private void AttackAnimUpdate()
    {
        //playerBase.animator.SetInteger("AttackCount", playerBase.attackController.attackCount);
        isAttackAnimPlaying = true;
    }
    private void AttackAnimEnd()
    {
        isAttackAnimPlaying = false;
    }

    private void OnDisable()
    {
        /*if( playerBase != null )
            playerBase.attackController.onAttackStarted -= AttackAnimUpdate;*/
    }

}
