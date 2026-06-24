using System;
using UnityEngine;

public class PlayerStatModel : IPlayerStatModel
{
	public int currentHP { get; private set; }

	public float finalTakeDamageMultiply { get; private set; }

	public float finalPhysicsAttack { get; private set; }

	public float finalMagicAttack { get; private set; }

	public float finalAttackSpeed { get; private set; }

	public float finalMoveSpeed { get; private set; }

	public float finalConcentrationSpeed { get; private set; }

	public float finalSkillCoolDownSpeed { get; private set; }

	public float finalSwapCoolDownSpeed { get; private set; }

	public float finalQuintessenceCoolDownSpeed { get; private set; }

	public float finalCriticalProbablility { get; private set; }

	public float finalCriticalDamageMultiply { get; private set; }

	public float finalJumpForce { get; private set; }

	public float finalFallMultiply { get; private set; }

	public int finalJumpMaxCount { get; private set; }

	public float finalDashForce { get; private set; }

	public float finalDashCoolTime { get; private set; }

	public float finalDashDuration { get; private set; }

	public int finalDashMaxCount { get; private set; }

	public event Action<int> OnChangeHp;
	public event Action OnStatCaculated;

	private DefaultStatData _defaultStatData;
	private SkulStatData _currentSkulStatData;

	public void SetDefaultStatData(DefaultStatData defaultStatData)=> _defaultStatData = defaultStatData;
	
	public void UpdateFinalStat(DefaultStatData defaultStatData, SkulStatData currentSkulStatData)
	{
		if (defaultStatData == null || currentSkulStatData == null) return;
		
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

		OnStatCaculated?.Invoke();

	}
	public void TakeDamage(int damage)
	{
		if(currentHP != 0)
		{
			currentHP -= damage;
		}
		OnChangeHp?.Invoke(currentHP);
	}

	public void SetSkulStatData(SkulStatData currentSkulStatData)
	{
		_currentSkulStatData = currentSkulStatData;

		UpdateFinalStat(_defaultStatData, _currentSkulStatData);
	}
		
}
