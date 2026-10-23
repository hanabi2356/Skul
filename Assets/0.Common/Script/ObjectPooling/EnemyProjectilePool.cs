using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyProjectilePool : IEnemyProjectilePool
{
	private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
	private readonly Dictionary<string, Queue<IEnemyProjectile>> _inactive = new Dictionary<string, Queue<IEnemyProjectile>>();
	private readonly Dictionary<IEnemyProjectile, string> _addressByInstance = new Dictionary<IEnemyProjectile, string>();
	private readonly List<AsyncOperationHandle<GameObject>> _handles = new List<AsyncOperationHandle<GameObject>>();
	private Transform _root;

	private Transform Root
	{
		get
		{
			if (_root == null)
			{
				_root = new GameObject("EnemyProjectilePool").transform;
			}
			return _root;
		}
	}

	public IEnemyProjectile Get(string address)
	{
		if (string.IsNullOrEmpty(address)) return null;

		IEnemyProjectile projectile;
		if (_inactive.TryGetValue(address, out var queue) && queue.Count > 0)
		{
			projectile = queue.Dequeue();
			projectile.Root.SetActive(true);
		}
		else
		{
			projectile = Create(address);
		}

		if (projectile == null) return null;

		projectile.BindPool(this);
		return projectile;
	}

	public void Release(IEnemyProjectile projectile)
	{
		if (projectile == null || projectile.Root == null) return;
		if (_addressByInstance.TryGetValue(projectile, out var address) == false) return;

		projectile.Root.SetActive(false);
		projectile.Root.transform.SetParent(Root);

		if (_inactive.TryGetValue(address, out var queue) == false)
		{
			queue = new Queue<IEnemyProjectile>();
			_inactive[address] = queue;
		}

		queue.Enqueue(projectile);
	}

	private IEnemyProjectile Create(string address)
	{
		GameObject prefab = LoadPrefab(address);
		if (prefab == null) return null;

		GameObject instance = Object.Instantiate(prefab, Root);

		var projectile = instance.GetComponent<IEnemyProjectile>();
		if (projectile == null)
		{
			Object.Destroy(instance);
			return null;
		}

		_addressByInstance[projectile] = address;
		return projectile;
	}

	private GameObject LoadPrefab(string address)
	{
		if (_prefabs.TryGetValue(address, out var prefab) && prefab != null) return prefab;
		var handle = Addressables.LoadAssetAsync<GameObject>(address);

		prefab = handle.WaitForCompletion();
		if (prefab == null) return null;
		_prefabs[address] = prefab;
		_handles.Add(handle);

		if (_inactive.ContainsKey(address) == false)
		{
			_inactive[address] = new Queue<IEnemyProjectile>();
		}
		return prefab;
	}
}
