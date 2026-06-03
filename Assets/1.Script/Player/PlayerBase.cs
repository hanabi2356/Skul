using System;
using Unity.VisualScripting;
using UnityEngine;

public enum EPlayerState
{
    Idle=0,
    Move=1,
    Jump=2,
    Attack=3,
    Dash=4,
    Hit=5,
    Dead=6
}
public class PlayerBase : MonoBehaviour
{
    
    private IPlayerState currentPlayerState;

    [SerializeField] private SkulStatData currentSkulStatData;
    [SerializeField] private DefaultStatData defaultStatData;
    [field : SerializeField]public EPlayerState currentPlayerStateEnum { get; private set; } = EPlayerState.Idle;

    //State
    public IPlayerState baseState { get; private set; }
    public IPlayerState idleState { get; private set; }
    public IPlayerState moveState { get; private set; }
    public IPlayerState attackState { get; private set; }
    public IPlayerState dashState { get; private set; }
    public IPlayerState hitState { get; private set; }
    public IPlayerState deadState { get; private set; }
    public IPlayerState jumpState { get; private set; }

    //Controller
    public PlayerMoveController moveController { get; private set; }
    public PlayerAnimController animController { get; private set; }
    public PlayerAttackController attackController { get; private set; }

    public PhysicsHandler physicsHandler { get; private set; }

    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }

    //기본 정보
    [HideInInspector] public int currentHP;
    [HideInInspector] public float finalTakeDamageMultiply;
    //공격력
    [HideInInspector] public float finalPhysicsAttack;
    [HideInInspector] public float finalMagicAttack;
    //공격, 이동, 정신집중 속도
    [HideInInspector] public float finalAttackSpeed;
    [HideInInspector] public float finalMoveSpeed;
    [HideInInspector] public float finalConcentrationSpeed;
    //쿨다운 속도
    [HideInInspector] public float finalSkillCoolDownSpeed;
    [HideInInspector] public float finalSwapCoolDownSpeed;
    [HideInInspector] public float finalQuintessenceCoolDownSpeed;
    //치명타 확률 및 치명타 데미지 배수
    [HideInInspector] public float finalCriticalProbablility;
    [HideInInspector] public float finalCriticalDamageMultiply;
    //점프
    [HideInInspector] public float finalJumpForce;
    [HideInInspector] public float finalFallMultiply;
    [HideInInspector] public int finalJumpMaxCount;
    //대쉬
    [HideInInspector] public float finalDashForce;
    [HideInInspector] public float finalDashCoolTime;
    [HideInInspector] public float finalDashDuration;
    [HideInInspector] public int finalDashMaxCount;
    

    private void Start()
    {
        InitStates();
        ChangeState(idleState, EPlayerState.Idle);

    }
    void Awake()
    {
        SkulStatDataLoader("LittleBorn");
        InitFinalStat();
        InitComponents();
    }
    /// <summary>
    /// 상태 초기화
    /// </summary>
    private void InitStates()
    {
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        dashState = new PlayerDashState(this);
        hitState = new PlayerHitState(this);
        deadState = new PlayerDeadState(this);
        jumpState = new PlayerJumpState(this);

        (idleState as BaseState)?.SetupTransitions();
        (moveState as BaseState)?.SetupTransitions();
        (attackState as BaseState)?.SetupTransitions();
        (dashState as BaseState)?.SetupTransitions();
        (jumpState as BaseState)?.SetupTransitions();
        //hit
        //dead

    }
    private void InitComponents()
    {
        if (moveController == null)
            moveController = GetComponent<PlayerMoveController>();

        if (animController == null)
            animController = GetComponent<PlayerAnimController>();

        if (attackController == null)
            attackController = GetComponent<PlayerAttackController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (body == null)
            body = GetComponent<Rigidbody2D>();

        if (physicsHandler == null)
            physicsHandler = GetComponent<PhysicsHandler>();
    }
    void Update()
    {
        currentPlayerState?.Execute();

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
    public void ChangeSkul(SkulStatData newData)
    {
        currentSkulStatData = newData;
    }
    public void SkulStatDataLoader(string name)
    {
        string path = "Data/Skul/"+name+"_Stat";
        currentSkulStatData = Resources.Load<SkulStatData>(path);
        if(currentSkulStatData == null)
        {
            Debug.LogError($"SkulStat 로딩 실패 [경로 : {path}]");
        }
        else
        {
            Debug.Log("Load 성공");
        }
    }
    /// <summary>
    /// defaultStatData와 currentSkulStatData를 가지고 최종 스탯을 결정하는 함수
    /// </summary>
    public void InitFinalStat()
    {
        currentHP = defaultStatData.GetHP;
        finalTakeDamageMultiply = defaultStatData.GetTakeDamageMultyply * currentSkulStatData.GetTakeDamageMultiply;

        finalPhysicsAttack = defaultStatData.GetPhysicsAttack * currentSkulStatData.GetPhysicalAttack;
        finalMagicAttack = defaultStatData.GetMagicAttack * currentSkulStatData.GetMagicAttack;

        finalAttackSpeed = defaultStatData.GetAttackSpeed * currentSkulStatData.GetAttackSpeed;
        finalMoveSpeed = defaultStatData.GetMoveSpeed * currentSkulStatData.GetMoveSpeed;
        finalConcentrationSpeed = defaultStatData.GetConcentrationSpeed *currentSkulStatData.GetConcentrationSpeed;

        finalSkillCoolDownSpeed = defaultStatData.GetSkillCoolDown * currentSkulStatData.GetSkillCoolDownSpeed;
        finalSwapCoolDownSpeed = defaultStatData.GetSwpaCoolDown * currentSkulStatData.GetSwapCoolDownSpeed;
        finalQuintessenceCoolDownSpeed = defaultStatData.GetQuitessenceCoolDown * currentSkulStatData.GetQuintessenceCoolDownSpeed;

        finalCriticalProbablility = defaultStatData.GetCriticalProbablility * currentSkulStatData.GetCriticalProbablility;
        finalCriticalDamageMultiply = defaultStatData.GetCriticalDamageMultiply * currentSkulStatData.GetCriticalDamageMultiply;

        finalJumpForce = defaultStatData.GetJumpForce;
        finalFallMultiply = defaultStatData.GetFallMultiply;
        finalJumpMaxCount = currentSkulStatData.GetJumpMaxCount;

        finalDashForce = defaultStatData.GetDashForce;
        finalDashCoolTime = defaultStatData.GetDashCoolTime;
        finalDashDuration = defaultStatData.GetDashDuration;
        finalDashMaxCount = currentSkulStatData.GetDashMaxCount;

    }
}
