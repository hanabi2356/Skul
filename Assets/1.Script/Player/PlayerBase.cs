using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    private IPlayerState currentPlayerState;

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerDeadState deadState { get; private set; }


    private void Start()
    {
        ChangeState(idleState);
    }
    void Awake()
    {
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        dashState = new PlayerDashState(this);
        jumpState = new PlayerJumpState(this);
        hitState = new PlayerHitState(this);
        deadState = new PlayerDeadState(this);
    }

    void Update()
    {
        currentPlayerState?.Excute();
    }
    public void ChangeState(IPlayerState newState)
    {
        if (currentPlayerState == newState)
            return;

        currentPlayerState?.Exit();
        currentPlayerState = newState;
        currentPlayerState?.Enter();
    }


}
