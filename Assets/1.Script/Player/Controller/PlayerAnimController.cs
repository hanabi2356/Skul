using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerBase playerBase;
    void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
    }

    void LateUpdate()
    {
        ChangeAnim(playerBase.currentPlayerStateEnum);
    }
    private void ChangeAnim(EPlayerState state)
    {
        playerBase.animator.SetInteger("State", ((int)state));
    }
}
