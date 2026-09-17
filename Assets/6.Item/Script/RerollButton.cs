using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RerollButton : MonoBehaviour
{

	[SerializeField] private List<ItemShelf> _itemShelves;
	private StoreManager _storeManager;

	[Inject]
	private void Construct(StoreManager storeManager)
	{
		_storeManager = storeManager;
	}

	[ContextMenu("[Reroll]")]
	public void Reroll()
	{
		_storeManager.StockShelves(_itemShelves);
	}
}
