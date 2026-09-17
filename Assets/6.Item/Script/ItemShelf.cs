using UnityEngine;

public class ItemShelf : MonoBehaviour, IInteractable
{
	[SerializeField, Label("Item 소환 위치")] private Transform _itemSpawnPosition;
	[SerializeField] private ItemShelfHudView _hudView;

	private ItemData _itemData;
	private GameObject _spawnedItem;
	[SerializeField]private bool _isInteractable = false;
	public ItemData ItemData => _itemData;
	public bool IsInteractable => _isInteractable;

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
		//재화 소비 및 구매 가능 여부 검사 호출
		_isInteractable = true;
		
		Clear();
	}
}
