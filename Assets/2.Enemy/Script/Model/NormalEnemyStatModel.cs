using System;
using UnityEngine;

public class NormalEnemyStatModel : INormalEnemyStatModel
{
	public int CurrentHP { get; private set; }
	public bool IsDead => CurrentHP <= 0;

	public float FinalAttackRange { get; private set; }
	public float FinalAttackSpeed { get; private set; }
	public float FinalAttackCoolTime { get; private set; }
	public float FinalTraceRange { get; private set; }
	public int FinalDamage { get; private set; }
	public float FinalMoveSpeed { get; private set; }

	public event Action<int> OnHPChanged;

	public void TakeDamage(int damage)
	{
		if (IsDead || damage <= 0) return;

		CurrentHP = Mathf.Max(0, CurrentHP - damage);
		OnHPChanged?.Invoke(CurrentHP);
	}

	public void UpdateFinalStat(EnemyStatDataDTO data)
	{
		CurrentHP = data.MaxHP;
		FinalAttackRange = data.AttackRange;
		FinalAttackSpeed = data.AttackSpeed;
		FinalAttackCoolTime = data.AttackCoolTime;
		FinalTraceRange = data.DetectedRange;
		FinalDamage = (int)data.AttackPower;
		FinalMoveSpeed = data.MoveSpeed;
	}
}
