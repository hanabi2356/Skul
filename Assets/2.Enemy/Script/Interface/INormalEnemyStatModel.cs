using System;

public interface INormalEnemyStatModel
{
	int CurrentHP { get; }
	bool IsDead { get; }
	float FinalAttackRange { get; }
	float FinalAttackSpeed { get; }
	float FinalAttackCoolTime { get; }
	float FinalTraceRange { get; }
	int FinalDamage { get; }
	float FinalMoveSpeed { get; }
	event Action<int> OnHPChanged;
	void TakeDamage(int damage);
	void UpdateFinalStat(EnemyStatDataDTO data);
}
