using UnityEngine;

public interface INormalEnemyAttackProjectile
{
	public GameObject Root { get; }
	public void Initialize(int damage, Vector2 direction, Vector2 spawnPosition);
	public void BindPool(NormalEnemyProjectilePool pool);
}
