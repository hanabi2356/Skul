using UnityEngine;

public interface IEnemyProjectilePool 
{
	public IEnemyProjectile Get(string address);
	public void Release(IEnemyProjectile projectile);
}
