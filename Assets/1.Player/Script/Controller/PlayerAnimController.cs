using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerBase playerBase;
	private IPlayerView _view;
    [Header("확인용 변수(조작 X)")]
    [field:SerializeField] public bool isAttackAnimPlaying { get; private set; } = false;
    void Awake()
    {
        if(playerBase == null)
            playerBase = GetComponent<PlayerBase>();

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
        ChangeAnim(playerBase.currentPlayerStateEnum);
        
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
