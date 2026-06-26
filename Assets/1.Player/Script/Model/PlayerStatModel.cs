using System;
using UnityEngine;

public class PlayerStatModel : IPlayerStatModel
{
	public int CurrentHP { get; private set; }

	public float FinalTakeDamageMultiply { get; private set; }

	public float FinalPhysicsAttack { get; private set; }

	public float FinalMagicAttack { get; private set; }

	public float FinalAttackSpeed { get; private set; }

	public float FinalMoveSpeed { get; private set; }

	public float FinalConcentrationSpeed { get; private set; }

	public float FinalSkillCoolDownSpeed { get; private set; }

	public float FinalSwapCoolDownSpeed { get; private set; }

	public float FinalQuintessenceCoolDownSpeed { get; private set; }

	public float FinalCriticalProbablility { get; private set; }

	public float FinalCriticalDamageMultiply { get; private set; }

	public float FinalJumpForce { get; private set; }

	public float FinalFallMultiply { get; private set; }

	public int FinalJumpMaxCount { get; private set; }

	public float FinalDashForce { get; private set; }

	public float FinalDashCoolTime { get; private set; }

	public float FinalDashDuration { get; private set; }

	public int FinalDashMaxCount { get; private set; }

	public float FinalAttackCountResetDelay { get; private set; }

	public int FinalMaxAttackCount { get; private set; }

	public float FinalInputBufferTime { get; private set; }

	public event Action<int> OnChangeHp;
	public event Action OnStatCaculated;

	private DefaultStatData _defaultStatData;
	private SkulStatData _currentSkulStatData;

	public void SetDefaultStatData(DefaultStatData defaultStatData)=> _defaultStatData = defaultStatData;
	
	public void UpdateFinalStat(DefaultStatData defaultStatData, SkulStatData currentSkulStatData)
	{
		if (defaultStatData == null || currentSkulStatData == null) return;
		
		CurrentHP = defaultStatData.HP;
		FinalTakeDamageMultiply = defaultStatData.TakeDamageMultyply * currentSkulStatData.TakeDamageMultiply;

		FinalPhysicsAttack = defaultStatData.PhysicsAttack * currentSkulStatData.PhysicalAttack;
		FinalMagicAttack = defaultStatData.MagicAttack * currentSkulStatData.MagicAttack;

		FinalAttackSpeed = defaultStatData.AttackSpeed * currentSkulStatData.AttackSpeed;
		FinalMoveSpeed = defaultStatData.MoveSpeed * currentSkulStatData.MoveSpeed;
		FinalConcentrationSpeed = defaultStatData.ConcentrationSpeed * currentSkulStatData.ConcentrationSpeed;

		FinalSkillCoolDownSpeed = defaultStatData.SkillCoolDown * currentSkulStatData.SkillCoolDownSpeed;
		FinalSwapCoolDownSpeed = defaultStatData.SwpaCoolDown * currentSkulStatData.SwapCoolDownSpeed;
		FinalQuintessenceCoolDownSpeed = defaultStatData.QuitessenceCoolDown * currentSkulStatData.QuintessenceCoolDownSpeed;

		FinalCriticalProbablility = defaultStatData.CriticalProbablility * currentSkulStatData.CriticalProbablility;
		FinalCriticalDamageMultiply = defaultStatData.CriticalDamageMultiply * currentSkulStatData.CriticalDamageMultiply;

		FinalJumpForce = defaultStatData.JumpForce;
		FinalFallMultiply = defaultStatData.FallMultiply;
		FinalJumpMaxCount = currentSkulStatData.JumpMaxCount;

		FinalDashForce = defaultStatData.DashForce;
		FinalDashCoolTime = defaultStatData.DashCoolTime;
		FinalDashDuration = defaultStatData.DashDuration;
		FinalDashMaxCount = currentSkulStatData.DashMaxCount;

		FinalAttackCountResetDelay = defaultStatData.AttackCountResetDelay;
		FinalMaxAttackCount = defaultStatData.MaxAttackCount;
		FinalInputBufferTime= defaultStatData.InputBufferTime;

		OnStatCaculated?.Invoke();

	}
	public void TakeDamage(int damage)
	{
		if(CurrentHP != 0)
		{
			CurrentHP -= damage;
		}
		OnChangeHp?.Invoke(CurrentHP);
	}

	public void SetSkulStatData(SkulStatData currentSkulStatData)
	{
		_currentSkulStatData = currentSkulStatData;

		UpdateFinalStat(_defaultStatData, _currentSkulStatData);
	}
		
}
