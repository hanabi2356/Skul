using UnityEngine;

public class RangeAttackAction : INormalEnemyAttackAction
{
	private readonly INormalEnemyView _view;
	private readonly INormalEnemyStatModel _statModel;
	private readonly NormalEnemyProjectilePool _pool;
	private int _projectileShotCount;

	public RangeAttackAction(INormalEnemyView view, 
		INormalEnemyStatModel statModel, 
		NormalEnemyProjectilePool pool)
	{
		_view = view;
		_statModel = statModel;
		_pool = pool;
	}

	public void Execute()
	{
		if (_projectileShotCount >= _view.ProjectileCount) return;

		SpawnProjectile();
		_projectileShotCount++;
	}

	public void Reset()
	{
		_projectileShotCount = 0;
	}

	private void SpawnProjectile()
	{
		Vector2 spawnPosition=_view.ProjectileSpawnTransform != null ? 
			(Vector2)_view.ProjectileSpawnTransform.position : (Vector2)_view.NormalEnemyTransform.position;

		Vector2 direction = GetDirection(spawnPosition);
		if (direction.sqrMagnitude <= 0.0001f) return;

		var projectile = _pool.Get(_view.ProjectileAddress);
		if(projectile == null) return;

		projectile.Initialize(_statModel.FinalDamage, direction, spawnPosition);

	}

	private Vector2 GetDirection(Vector2 spawnPosition)
	{
		if(_view.AimAtTarget)
		{
			return (_view.TargetPosition - spawnPosition).normalized;
		}

		return _view.NormalEnemyTransform.right;
	}
}
