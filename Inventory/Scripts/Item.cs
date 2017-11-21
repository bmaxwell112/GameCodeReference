using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemValue;
	public ItemType itemType;

	public enum ItemType
	{
		ForSale,
		ForShip,
		Quest
	}

	public Item(string name, int id, string desc, int value, ItemType type)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
		itemValue = value;
		itemType = type;
	}
	public Item()
	{
		itemID = -1;
	}
}
