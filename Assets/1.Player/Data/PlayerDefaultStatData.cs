using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatData", menuName = "Create Default Stat Data/DefaultStatData")]
public class DefaultStatData : ScriptableObject
{
    
    [SerializeField] private int _hp;
    [SerializeField] private float _takeDamageMultyply;

    [SerializeField] private float _physicsAttack;
    [SerializeField] private float _magicAttack;

    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _concentrationSpeed;

    [SerializeField] private float _skillCoolDown;
    [SerializeField] private float _swapCoolDown;
    [SerializeField] private float _quintessenceCoolDown; 

    [SerializeField] private float _criticalProbablility;
    [SerializeField] private float _criticalDamageMultiply;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiply;
    

    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashCoolTime;
    [SerializeField] private float _dashDuration;
    

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

}
