using TMPro;
using UnityEngine;
public enum SkulType
{
    Speed,
    Balance,
    Power
}
public enum SkulRarity
{
    Common,
    Rare,
    Unique,
    Legendary
}
/// <summary>
/// 값을 프로퍼티가 아닌 [SerializeField] private를 이용해 저장한 이유
/// 1. 데이터 무결성 : 모든 필드를 [SerializeField] private으로 선언하여 외부에서의 직접적인 수정을 차단하고 
/// Unity Insperctor를 통한 데이터 시각화
/// 2. 직렬화 효율 : Unity의 직렬화 시스템이 필드 기반으로 동작함을 고려하여 프로퍼티 대신 필드를 사용해 CSV파일을 
/// 리플렉션을 통해 안정적으로 주입할 수 있게 설계                  
/// 3. 읽기 전용 제공 : 외부 로직에서 람다 프로퍼티를 통해서만 접근할 수 있게하여 런타임 중 원본 데이터의 오염을 방지
/// </summary>
[CreateAssetMenu(fileName = "SkulStatData", menuName = "Create Data File/SkulStatData")]
public class SkulStatData : ScriptableObject
{
    [Header("Skul Data")]
    [SerializeField] private string _name; //이름
    [SerializeField] private SkulType _type; //타입
    [SerializeField] private SkulRarity _rarity; //등급

    [Header("Basic Data")]
    [SerializeField] private int _hp; //체력
    [SerializeField] private float _takeDamageMultiply; //받는 데미지 배수

    [Header("Damage Data")]
    [SerializeField] private float _physicalAttack; //물리 데미지
    [SerializeField] private float _magicAttack; //마법 데미지

    [Header("Speed")]
    [SerializeField] private float _attackSpeed; //공격 속도
    [SerializeField] private float _moveSpeed; //이동 속도
    [SerializeField] private float _concentrationSpeed; //정신집중 속도

    [Header("Cool Down Speed")]
    [SerializeField] private float _skillCoolDownSpeed; //스킬 쿨다운 속도
    [SerializeField] private float _swapCoolDownSpeed; //교체 쿨다운 속도
    [SerializeField] private float _quintessenceCoolDownSpeed; //정수 쿨다운 속도

    [Header("Critical")]
    [SerializeField] private float _criticalProbablility; //크리티컬 확률
    [SerializeField] private float _criticalDamageMultiply; //크리티컬 데미지 배수

    public string Name => _name;
    public SkulType Type => _type;
    public SkulRarity Rarity => _rarity;
    public int HP => _hp;
    public float TakeDamageMultiply => _takeDamageMultiply;
    public float PhysicalAttack => _physicalAttack;
    public float MagicAttack => _magicAttack;  
    public float AttackSpeed => _attackSpeed;
    public float MoveSpeed => _moveSpeed;
    public float ConcentrationSpeed => _concentrationSpeed;
    public float SkillCoolDownSpeed => _skillCoolDownSpeed;
    public float SwapCoolDownSpeed => _swapCoolDownSpeed;
    public float QuintessenceCoolDownSpeed => _quintessenceCoolDownSpeed;
    public float CriticalProbablility => _criticalProbablility;
    public float CriticalDamageMultiply => _criticalDamageMultiply;

}
