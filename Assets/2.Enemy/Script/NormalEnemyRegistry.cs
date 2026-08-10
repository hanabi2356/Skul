using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyRegistry 
{
	private readonly HashSet<NormalEnemyPresenter> _enemies = new HashSet<NormalEnemyPresenter>();
	public IReadOnlyCollection<NormalEnemyPresenter> Enemies => _enemies;

	public void Registry(NormalEnemyPresenter enemy) => _enemies.Add(enemy);
	public void Unregistry(NormalEnemyPresenter enemy) => _enemies.Remove(enemy);
}
