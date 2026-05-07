using System;
using UnityEngine;

public enum EPlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
    Dash,
    Hit,
    Dead
}
public class PlayerBase : MonoBehaviour
{

    private IPlayerState currentPlayerState;
    [field : SerializeField]public EPlayerState currentPlayerStateEnum { get; private set; } = EPlayerState.Idle;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }

    public PlayerMoveController moveController { get; private set; }
    public PlayerInputController inputController { get; private set; }
    public PlayerAnimController animController { get; private set; }

    public PhysicsHandler physicsHandler { get; private set; }

    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }

    private void Start()
    {
        ChangeState(idleState, currentPlayerStateEnum);
    }
    void Awake()
    {
        SkulInit();
        body.gravityScale = 2.5f;
    }

    private void SkulInit()
    {
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        dashState = new PlayerDashState(this);
        hitState = new PlayerHitState(this);
        deadState = new PlayerDeadState(this);
        jumpState = new PlayerJumpState(this);

        if (moveController == null)
            moveController = GetComponent<PlayerMoveController>();
        if (inputController == null)
            inputController = GetComponent<PlayerInputController>();
        if (animController == null)
            animController = GetComponent<PlayerAnimController>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        if (body == null)
            body = GetComponent<Rigidbody2D>();
        if(physicsHandler == null)
            physicsHandler = GetComponent<PhysicsHandler>();
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
