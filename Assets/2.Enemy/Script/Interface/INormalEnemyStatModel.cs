using System;

public interface INormalEnemyStatModel
{
	public int CurrentHP { get; }
	public bool IsDead { get; }
	public float FinalAttackRange { get; }
	public float FinalAttackSpeed { get; }
	public float FinalAttackCoolTime { get; }
	public float FinalTraceRange { get; }
	public int FinalDamage { get; }
	public float FinalMoveSpeed { get; }
	public AttackType FinalAttackType { get; }

	event Action<int> OnHPChanged;
	void TakeDamage(int damage);
	void UpdateFinalStat(EnemyStatDataDTO data);
}
