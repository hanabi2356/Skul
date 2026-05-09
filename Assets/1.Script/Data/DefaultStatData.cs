using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStateData", menuName = "Scriptable Objects/DefaultStateData")]
public class DefaultStatData : ScriptableObject
{
    [SerializeField] private int defHP;
    [SerializeField] private float defTakeDamageMultyply;

    [SerializeField] private float defPhysicsAttack;
    [SerializeField] private float defMagicAttack;

    [SerializeField] private float defAttackSpeed;
    [SerializeField] private float defMoveSpeed;
    [SerializeField] private float defConcentrationSpeed;

    [SerializeField] private float defSkillCoolDown;
    [SerializeField] private float defSwapCoolDown;
    [SerializeField] private float defQuintessenceCoolDown; 

    [SerializeField] private float defCriticalProbablility;
    [SerializeField] private float defCriticalDamageMultiply;

    public int GetDefHP => defHP;
    public float GetDefTakeDamageMultyply => defTakeDamageMultyply;
    public float GetDefPhysicsAttack => defPhysicsAttack;
    public float GetDefMagicAttack => defMagicAttack;
    public float GetDefAttackSpeed => defAttackSpeed;
    public float GetDefMoveSpeed => defMoveSpeed;
    public float GetDefConcentrationSpeed => defConcentrationSpeed;
    public float GetDefSkillCoolDown => defSkillCoolDown;
    public float GetDefSwpaCoolDown => defSwapCoolDown;
    public float GetDefQuitessenceCoolDown => defQuintessenceCoolDown;
    public float GetDefCriticalProbablility => defCriticalProbablility;
    public float GetDefCriticalDamageMultiply => defCriticalDamageMultiply;
}
