using UnityEngine;

public class BossSkillProjectileAttack : BossSkillBase
{
	private const int _projectileCount = 3;
	private const float _spreadAngle = 25.0f;
	private const float _forwardOffset = 0.8f;
	private const string _projectileAddress = "Object/BossProjectile";

	private readonly IEnemyProjectilePool _pool;
	public BossSkillProjectileAttack(IBossEnemyView view, IEnemyStatModel statModel, IEnemyProjectilePool pool) : base(view, statModel)
	{
		_pool = pool;
	}

	public override int SkillNumber => 4;
	protected override float CoolTime => 8.0f;
	public override float MaxUseRange => 10.0f;
	
	protected override bool OnApplyHit()
	{
		if (_view.BossEnemyTransform == null) return false;
		
		Vector2 origin = _view.BossEnemyTransform.position;
		Vector2 forward = Vector2.right * _view.LookDirection;
		float center = (_projectileCount - 1) * 0.5f;
		bool spawned = false;


		for (int i = 0; i < _projectileCount; i++)
		{
			float angle = (i - center) * _spreadAngle;
			Vector2 direction = Quaternion.Euler(0.0f, 0.0f, angle) * forward;
			Vector2 spawnPosition = origin + forward * _forwardOffset;

			var projectile = _pool.Get(_projectileAddress);
			if (projectile == null) continue;

			projectile.Initialize(_statModel.FinalDamage, direction, spawnPosition);
			spawned = true;
		}
		return spawned;
	}

	
}
