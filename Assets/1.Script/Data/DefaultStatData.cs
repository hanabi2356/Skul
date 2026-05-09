using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatData", menuName = "Create Default Stat Data/DefaultStatData")]
public class DefaultStatData : ScriptableObject
{
    [Header("기본 스탯 수치")]
    [SerializeField] private int HP;
    [SerializeField] private float takeDamageMultyply;

    [SerializeField] private float physicsAttack;
    [SerializeField] private float magicAttack;

    [SerializeField] private float attackSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float concentrationSpeed;

    [SerializeField] private float skillCoolDown;
    [SerializeField] private float swapCoolDown;
    [SerializeField] private float quintessenceCoolDown; 

    [SerializeField] private float criticalProbablility;
    [SerializeField] private float criticalDamageMultiply;

    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiply;
    [SerializeField] private float jumpMaxCount;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashCoolTime;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashMaxCount;

    public int GetHP => HP;
    public float GetTakeDamageMultyply => takeDamageMultyply;
    public float GetPhysicsAttack => physicsAttack;
    public float GetMagicAttack => magicAttack;
    public float GetAttackSpeed => attackSpeed;
    public float GetMoveSpeed => moveSpeed;
    public float GetConcentrationSpeed => concentrationSpeed;
    public float GetSkillCoolDown => skillCoolDown;
    public float GetSwpaCoolDown => swapCoolDown;
    public float GetQuitessenceCoolDown => quintessenceCoolDown;
    public float GetCriticalProbablility => criticalProbablility;
    public float GetCriticalDamageMultiply => criticalDamageMultiply;
}
