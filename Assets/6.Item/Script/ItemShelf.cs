using UnityEngine;
using Zenject;

public class ItemShelf : MonoBehaviour, IInteractable
{
	[SerializeField, Label("Item 소환 위치")] private Transform _itemSpawnPosition;
	[SerializeField] private ItemShelfHudView _hudView;

	private ItemData _itemData;
	private GameObject _spawnedItem;
	[SerializeField]private bool _isInteractable = false;
	public ItemData ItemData => _itemData;
	public bool IsInteractable => _isInteractable;

	private  ICurrencyModel _currencyModel;

	[Inject]
	public void Construct(ICurrencyModel currencyModel)
	{
		_currencyModel = currencyModel;
	}
	public void SetItem(ItemData data)
	{
		Clear();
		_itemData = data;
		if (data == null) return;

		if(data.ItemPrefab != null)
		{
			_spawnedItem = Instantiate(data.ItemPrefab, _itemSpawnPosition);
		}

		if(_hudView != null)
		{
			_hudView.SetPrice(data.ItemPrice);
		}
	}

	public void Clear()
	{
		_itemData = null;
		if(_spawnedItem != null)
		{
			Destroy(_spawnedItem);
			_spawnedItem = null;
		}

		if(_hudView != null)
		{
			_hudView.Hide();
		}
	}

	public void Interact()
	{
		if (_isInteractable) return;
		if (_itemData == null) return;

		if (_currencyModel.TrySpend(ECurrencyType.Gold, _itemData.ItemPrice) == false) return;

		_isInteractable = true;
		Clear();
	}
}
