using System;
using Unity.VisualScripting;
using UnityEngine;

public enum EPlayerState
{
    Idle = 0,
    Move = 1,
    Jump = 2,
    Attack = 3,
    Dash = 4,
    Hit = 5,
    Dead = 6
}
public class PlayerBase : MonoBehaviour
{

    private IState currentPlayerState;

    [SerializeField] private SkulStatData currentSkulStatData;
    [SerializeField] private DefaultStatData defaultStatData;
    [field: SerializeField] public EPlayerState currentPlayerStateEnum { get; private set; } = EPlayerState.Idle;

    //State
    public IState baseState { get; private set; }
    public IState idleState { get; private set; }
    public IState moveState { get; private set; }
    public IState attackState { get; private set; }
    public IState dashState { get; private set; }
    public IState hitState { get; private set; }
    public IState deadState { get; private set; }
    public IState jumpState { get; private set; }

    //Controller
    public PlayerMoveController moveController { get; private set; }
    public PlayerAnimController animController { get; private set; }
    public PlayerAttackController attackController { get; private set; }

    public PlayerPhysicsHandler physicsHandler { get; private set; }

    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }

    //기본 정보
    public int currentHP { get; private set; }
    public float finalTakeDamageMultiply { get; private set; }
	//공격력
	public float finalPhysicsAttack { get; private set; }
	public float finalMagicAttack{ get; private set; }
	//공격, 이동, 정신집중 속도
	public float finalAttackSpeed { get; private set; }
	public float finalMoveSpeed { get; private set; }
	public float finalConcentrationSpeed { get; private set; }
	//쿨다운 속도
	public float finalSkillCoolDownSpeed { get; private set; }
	public float finalSwapCoolDownSpeed { get; private set; }
	public float finalQuintessenceCoolDownSpeed { get; private set; }
	//치명타 확률 및 치명타 데미지 배수
	public float finalCriticalProbablility { get; private set; }
	public float finalCriticalDamageMultiply { get; private set; }
	//점프
	public float finalJumpForce { get; private set; }
	public float finalFallMultiply { get; private set; }
	public int finalJumpMaxCount { get; private set; }
	//대쉬
	public float finalDashForce { get; private set; }
	public float finalDashCoolTime { get; private set; }
	public float finalDashDuration { get; private set; }
	 public int finalDashMaxCount { get; private set; }


	private void Start()
    {
        //InitStates();
        //ChangeState(idleState, EPlayerState.Idle);

    }
    void Awake()
    {
        //SkulStatDataLoader("LittleBorn");
        //InitFinalStat();
        //InitComponents();
    }
    /// <summary>
    /// 상태 초기화 및 전이 조건 List에 추가
    /// </summary>
    private void InitStates()
    {
        /*idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        dashState = new PlayerDashState(this);
        hitState = new PlayerHitState(this);
        deadState = new PlayerDeadState(this);
        jumpState = new PlayerJumpState(this);

        (idleState as PlayerBaseState)?.SetupTransitions();
        (moveState as PlayerBaseState)?.SetupTransitions();
        (attackState as PlayerBaseState)?.SetupTransitions();
        (dashState as PlayerBaseState)?.SetupTransitions();
        (jumpState as PlayerBaseState)?.SetupTransitions();
        (hitState as PlayerBaseState)?.SetupTransitions();
        (deadState as PlayerBaseState)?.SetupTransitions();*/

    }
    private void InitComponents()
    {
        /*if (moveController == null)
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
            physicsHandler = GetComponent<PlayerPhysicsHandler>();*/
    }
    void Update()
    {
        currentPlayerState?.Execute();

    }
    public void ChangeState(IState newState, EPlayerState newStateEnum)
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
        string path = "Data/Skul/" + name + "_Stat";
        currentSkulStatData = Resources.Load<SkulStatData>(path);
        if (currentSkulStatData == null)
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
        currentHP = defaultStatData.HP;
        finalTakeDamageMultiply = defaultStatData.TakeDamageMultyply * currentSkulStatData.TakeDamageMultiply;

        finalPhysicsAttack = defaultStatData.PhysicsAttack * currentSkulStatData.PhysicalAttack;
        finalMagicAttack = defaultStatData.MagicAttack * currentSkulStatData.MagicAttack;

        finalAttackSpeed = defaultStatData.AttackSpeed * currentSkulStatData.AttackSpeed;
        finalMoveSpeed = defaultStatData.MoveSpeed * currentSkulStatData.MoveSpeed;
        finalConcentrationSpeed = defaultStatData.ConcentrationSpeed * currentSkulStatData.ConcentrationSpeed;

        finalSkillCoolDownSpeed = defaultStatData.SkillCoolDown * currentSkulStatData.SkillCoolDownSpeed;
        finalSwapCoolDownSpeed = defaultStatData.SwpaCoolDown * currentSkulStatData.SwapCoolDownSpeed;
        finalQuintessenceCoolDownSpeed = defaultStatData.QuitessenceCoolDown * currentSkulStatData.QuintessenceCoolDownSpeed;

        finalCriticalProbablility = defaultStatData.CriticalProbablility * currentSkulStatData.CriticalProbablility;
        finalCriticalDamageMultiply = defaultStatData.CriticalDamageMultiply * currentSkulStatData.CriticalDamageMultiply;

        finalJumpForce = defaultStatData.JumpForce;
        finalFallMultiply = defaultStatData.FallMultiply;
        finalJumpMaxCount = currentSkulStatData.JumpMaxCount;

        finalDashForce = defaultStatData.DashForce;
        finalDashCoolTime = defaultStatData.DashCoolTime;
        finalDashDuration = defaultStatData.DashDuration;
        finalDashMaxCount = currentSkulStatData.DashMaxCount;

    }
}
