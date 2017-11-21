using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();

	private ItemDatabase database;
	private bool showInventory, showTooltip, draggingItem;
	private string tooltip;
	private Item draggedItem;
	private int previousIndex;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add(new Item());
		}
		database = FindObjectOfType<ItemDatabase>();

		AddItem(0);
		AddItem(1);
		AddItem(2);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			showInventory = !showInventory;
		}
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
		{
			SaveInventory();
		}
		if (GUI.Button(new Rect(40, 450, 100, 40), "Load"))
		{
			LoadInventory();
		}
		tooltip = "";
		GUI.skin = skin;
		if (showInventory)
		{
			DrawInventory();
			if (showTooltip)
			{
				GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("Tooltip"));
			}
		}
		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}
		
	}

	void SaveInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			PlayerPrefs.SetInt("Inventory_" + i, inventory[i].itemID);
		}
	}

	void LoadInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventory[i] = PlayerPrefs.GetInt("Inventory_" + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory_" + i)] : new Item();
		}
	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++)
		{
			for (int x = 0; x < slotsX; x++)				
			{
				Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
				GUI.Box(slotRect, "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];
				if (slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition))
					{
						CreateTooltip(slots[i]);
						showTooltip = true;
						if (e.button == 0 && e.type == EventType.mouseDown && !draggingItem)
						{
							draggingItem = true;
							previousIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
						}
						if (e.type == EventType.mouseUp && draggingItem)
						{
							inventory[previousIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}										
				}
				else
				{
					if (slotRect.Contains(e.mousePosition))
					{
						if (e.type == EventType.mouseUp && draggingItem)
						{
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if (tooltip == "")
				{
					showTooltip = false;
				}
				i++;
			}
		}
	}

	string CreateTooltip(Item item)
	{
		tooltip = "<color=#000fff>" + item.itemName + "\n\n" + item.itemDesc + "\n\n\nValue: " + item.itemValue + "</color>";
		return tooltip;
	}

	void AddItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemName == null)
			{
				for (int j = 0; j < database.items.Count; j++)
				{
					if (database.items[j].itemID == id)
					{
						inventory[i] = database.items[j];
					}
				}				
				break;
			}
		}
	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		bool result = false;
		for (int i = 0; i < inventory.Count; i++)
		{
			result = inventory[i].itemID == id;
			if (result)
			{
				break;
			}
		}
		return result;
	}
}
