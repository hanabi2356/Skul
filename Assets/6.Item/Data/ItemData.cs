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
	/// <summary>
	/// CSV에 존재하는 데이터
	/// </summary>
	[SerializeField] private string _itemID;
	[SerializeField] private string _itemName;
	[SerializeField] private ItemRarity _itemRarity;
	[SerializeField] private int _itemPrice;


	[SerializeField] private Sprite _itemSprite;
	[SerializeField] private GameObject _itemPrefab;

	public Sprite ItemSprite => _itemSprite;
	public GameObject ItemPrefab => _itemPrefab;
	public string ItemID => _itemID;
	public string ItemName => _itemName;
	public ItemRarity ItemRarity => _itemRarity;
	public int ItemPrice => _itemPrice;



}
