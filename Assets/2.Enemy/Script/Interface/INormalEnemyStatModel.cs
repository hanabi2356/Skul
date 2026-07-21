using System;
using UnityEngine;

public interface INormalEnemyStatModel
{
    public int CurrentHP { get; }
	public float FinalAttackRange { get; }
	public float FinalAttackSpeed { get; }
	public float FinalAttackCoolTime { get; }
	public float FinalTraceRange { get; }
	public int FinalDamage { get; }
	public float FinalMoveSpeed { get; }
	public event Action<float> OnHPChanged;
	public void TakeDamage(int damage);
	public void UpdateFinalStat(EnemyStatData data);
	

}
