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
[CreateAssetMenu(fileName = "SkulStatData", menuName = "Scriptable Objects/SkulData")]
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

}
