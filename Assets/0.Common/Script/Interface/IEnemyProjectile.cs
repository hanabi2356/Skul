using UnityEngine;

public interface IEnemyProjectile 
{
	public GameObject Root { get; }
	public void Initialize(int damage, Vector2 direction, Vector2 spawnPosition);
	public void BindPool(IEnemyProjectilePool pool);
}
