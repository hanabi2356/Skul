using UnityEngine;

public enum EPlayerState
{
    Idle,
    Move,
    Attack,
    Dash,
    Hit,
    Dead
}
public class PlayerBase : MonoBehaviour
{

    private IPlayerState currentPlayerState;
    public EPlayerState currentPlayerStateEnum { get; private set; } = EPlayerState.Idle;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerDeadState deadState { get; private set; }


    private void Start()
    {
        ChangeState(idleState, currentPlayerStateEnum);
    }
    void Awake()
    {
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        dashState = new PlayerDashState(this);
        hitState = new PlayerHitState(this);
        deadState = new PlayerDeadState(this);
    }

    void Update()
    {
        currentPlayerState?.Excute();
    }
    public void ChangeState(IPlayerState newState, EPlayerState newStateEnum)
    {
        if (currentPlayerState == newState)
            return;

        currentPlayerState?.Exit();
        currentPlayerState = newState;
        currentPlayerStateEnum = newStateEnum;
        currentPlayerState?.Enter();
    }


}
