using UnityEngine;

public enum EnemyRarity
{
    Common,
    Elite,
    Boss
}
public enum AttackType
{
    Melee,
    Ranged,
}

[CreateAssetMenu(fileName = "EnemyStatData", menuName = "Create DataFile/EnemyStatData")]
public class EnemyStatData : ScriptableObject
{
    [Header("General Data")]
    [SerializeField] private string _enemyID;
    [SerializeField] private string _name;
    [SerializeField] private EnemyRarity _enemyRarity;

    [Header("Stat Data")]
    [SerializeField] private float _maxHP;

    [Header("Attack Data")]
    [SerializeField] private AttackType _attackType;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackCoolTime;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _detectedRange;


    [Header("Move Data")]
    [SerializeField] private float _moveSpeed;


    //general data
    public string EnemyID => _enemyID;
    public string Name => _name;
    public EnemyRarity EnemyRarity => _enemyRarity;
    public AttackType AttackType => _attackType;

    //stat data
    public float MaxHP => _maxHP;

    //attack data
    public float AttackPower => _attackPower;
    public float AttackSpeed => _attackSpeed;
    public float AttackCoolTime => _attackCoolTime;
    public float AttackRange => _attackRange;
    public float DetectedRange => _detectedRange;
    
    //move data
    public float MoveSpeed => _moveSpeed;


}
