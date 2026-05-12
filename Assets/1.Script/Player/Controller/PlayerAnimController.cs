using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerBase playerBase;
    void Awake()
    {
        if(playerBase == null)
            playerBase = GetComponent<PlayerBase>();

    }

    private void Start()
    {
        if (playerBase != null)
            playerBase.attackController.onAttackStarted += AttackAnimUpdate;

    }

    void Update()
    {
        ChangeAnim(playerBase.currentPlayerStateEnum);
        
    }
    private void ChangeAnim(EPlayerState state)
    {
        playerBase.animator.SetInteger("State", ((int)state));
    }
    private void AttackAnimUpdate()
    {
        playerBase.animator.SetInteger("AttackCount", playerBase.attackController.attackCount);
    }


    private void OnDisable()
    {
        if( playerBase != null )
            playerBase.attackController.onAttackStarted -= AttackAnimUpdate;
    }

}
