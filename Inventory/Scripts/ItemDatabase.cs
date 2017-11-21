using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>();

	void Start()
	{
		items.Add(new Item("Pocket Knife", 0, "Cool Knife", 100, Item.ItemType.ForSale));
		items.Add(new Item("Rocks", 1, "You got a rock", 100, Item.ItemType.ForSale));
		items.Add(new Item("Rocks 2", 2, "You got more rocks", 100, Item.ItemType.ForSale));
	}
}
