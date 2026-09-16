using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StoreManager 
{
	private const string _itemLabel = "Equipment";

	private readonly List<ItemData> _itemPool = new List<ItemData>();
	private AsyncOperationHandle<IList<ItemData>> _handle;
	private bool _isLoaded;

	public void LoadItem()
	{
		if (_isLoaded) return;

		_handle = Addressables.LoadAssetsAsync<ItemData>(_itemLabel, null);
		IList<ItemData> loaded = _handle.WaitForCompletion();

		_itemPool.Clear();
		if(loaded != null)
		{
			_itemPool.AddRange(loaded);
		}

		_isLoaded = true;
	}

	public void StockShelves(IReadOnlyList<ItemShelf> shelves)
	{
		if (_isLoaded == false)
		{
			LoadItem();
		}

		if (_itemPool.Count == 0 || shelves == null || shelves.Count == 0) return;

		List<ItemData> picks = PickUniqueRandom(shelves.Count);
		for (int i = 0; i < shelves.Count; i++)
		{
			if(i<picks.Count)
			{
				shelves[i].SetItem(picks[i]);
			}
			else
			{
				shelves[i].Clear();
			}
		}
	}

	public void Release()
	{
		if (_isLoaded == false) return;

		Addressables.Release(_handle);
		_itemPool.Clear();
		_isLoaded = false;
	}

	private List<ItemData> PickUniqueRandom(int count)
	{
		List<ItemData> shuffled = new List<ItemData>(_itemPool);
		int n = shuffled.Count;
		for (int i = 0; i < n; i++)
		{
			int j = Random.Range(i, n);
			(shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
		}

		if (count > shuffled.Count)
		{
			count = shuffled.Count;
		}

		return shuffled.GetRange(0, count);
	}
		
}
