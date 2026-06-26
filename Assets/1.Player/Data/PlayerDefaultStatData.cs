using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatData", menuName = "Create Default Stat Data/DefaultStatData")]
public class DefaultStatData : ScriptableObject
{
    
    [SerializeField, Label("체력")] private int _hp;
    [SerializeField,Label("받는 데미지 ")] private float _takeDamageMultyply;

    [SerializeField,Label("물리 데미지")] private float _physicsAttack;
    [SerializeField, Label("마법 데미지")] private float _magicAttack;

    [SerializeField, Label("공격 속도")] private float _attackSpeed;
    [SerializeField, Label("이동 속도")] private float _moveSpeed;
    [SerializeField, Label("정신집중 속도")] private float _concentrationSpeed;

    [SerializeField,Label("스킬 쿨 다운 속도")] private float _skillCoolDown;
    [SerializeField,Label("교체 쿨 다운 속도")] private float _swapCoolDown;
    [SerializeField,Label("정수 쿨 다운 속도")] private float _quintessenceCoolDown; 

    [SerializeField, Label("치명타 확률")] private float _criticalProbablility;
    [SerializeField, Label("치명타 데미지 계수")] private float _criticalDamageMultiply;

    [SerializeField,Label("점프 속도")] private float _jumpForce;
    [SerializeField,Label("떨어지는 속도")] private float _fallMultiply;
    

    [SerializeField, Label("대시 속도")] private float _dashForce;
    [SerializeField, Label("대시 쿨타임")] private float _dashCoolTime;
    [SerializeField, Label("대시 지속시간")] private float _dashDuration;

	[SerializeField, Label("공격 횟수 초기화 시간")]private float _attackCountResetDelay = 0.5f;
	[SerializeField, Label("공격 횟수")]private int _maxAttackCount = 2;
	[SerializeField, Label("공격 입력 지연시간")]private float _inputBufferTime = 0.2f;

	public int HP => _hp;
    public float TakeDamageMultyply => _takeDamageMultyply;
    public float PhysicsAttack => _physicsAttack;
    public float MagicAttack => _magicAttack;
    public float AttackSpeed => _attackSpeed;
    public float MoveSpeed => _moveSpeed;
    public float ConcentrationSpeed => _concentrationSpeed;
    public float SkillCoolDown => _skillCoolDown;
    public float SwpaCoolDown => _swapCoolDown;
    public float QuitessenceCoolDown => _quintessenceCoolDown;
    public float CriticalProbablility => _criticalProbablility;
    public float CriticalDamageMultiply => _criticalDamageMultiply;
    public float JumpForce => _jumpForce;
    public float FallMultiply => _fallMultiply;
    public float DashForce => _dashForce;
    public float DashCoolTime => _dashCoolTime;
    public float DashDuration => _dashDuration;
	public float AttackCountResetDelay => _attackCountResetDelay;
	public int MaxAttackCount=>_maxAttackCount;
	public float InputBufferTime => _inputBufferTime;
}
