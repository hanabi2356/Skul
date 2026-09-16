using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StoreRoot : MonoBehaviour
{
	[SerializeField] private List<ItemShelf> _itemShelves;
	private StoreManager _storeManager;

	[Inject]
	private void Construct(StoreManager storeManager)
	{
		_storeManager = storeManager;
	}
   
    void Start()
    {
		_storeManager.StockShelves(_itemShelves);
    }

	private void OnDestroy()
	{
		_storeManager.Release();
	}
	
}
