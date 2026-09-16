using UnityEngine;

public class ItemShelf : MonoBehaviour
{
	[SerializeField, Label("Item 소환 위치")] private Transform _itemSpawnPosition;

	private ItemData _itemData;
	private GameObject _spawnedItem;

	public ItemData ItemData => _itemData;

	public void SetItem(ItemData data)
	{
		Clear();
		_itemData = data;
		if (data == null) return;

		if(data.ItemPrefab != null)
		{
			_spawnedItem = Instantiate(data.ItemPrefab, _itemSpawnPosition);
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
	}
   
}
