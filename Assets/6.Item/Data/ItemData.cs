using UnityEngine;

public enum ItemRarity
{
	Common,
	Rare,
	Unique,
	Legendary
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Create Scriptable Objects/Create ItemData")]
public class ItemData : ScriptableObject
{
	[SerializeField] private Sprite _itemSprite;
	[SerializeField] private GameObject _itemPrefab;
	[SerializeField] private string _itemID;
	[SerializeField] private string _itemName;
	[SerializeField] private ItemRarity _itemRarity;

	[SerializeField] private int _itemPrice;


	public Sprite ItemSprite => _itemSprite;
	public GameObject ItemPrefab => _itemPrefab;
	public string ItemID => _itemID;
	public string ItemName => _itemName;
	public ItemRarity ItemRarity => _itemRarity;
	public int ItemPrice => _itemPrice;



}
