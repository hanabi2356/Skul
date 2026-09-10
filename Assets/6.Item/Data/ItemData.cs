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
	[SerializeField] private string _itemID;
	[SerializeField] private string _itemName;
	[SerializeField] private ItemRarity _itemRarity;

	[SerializeField] private int _price;



	public string ItemID => _itemID;
	public string ItemName => _itemName;
	public ItemRarity ItemRarity => _itemRarity;
	public int Price => _price;



}
