using System;
using UnityEngine;

public class NormalEnemyStatModel : INormalEnemyStatModel
{
	public int CurrentHP { get; private set; }

	public float FinalAttackRange { get; private set; }

	public float FinalAttackSpeed { get; private set; }

	public float FinalAttackCoolTime { get; private set; }

	public float FinalTraceRange { get; private set; }

	public int FinalDamage { get; private set; }

	public float FinalMoveSpeed { get; private set; }

	public event Action<float> OnHPChanged;

	public void TakeDamage(int damage)
	{
		if(CurrentHP != 0)
		{
			CurrentHP -= damage;

		}
		OnHPChanged?.Invoke(CurrentHP);

	}

	public void UpdateFinalStat(EnemyStatData data)
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
