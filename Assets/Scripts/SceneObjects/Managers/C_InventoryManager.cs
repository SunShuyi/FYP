using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C_InventoryManager : MonoBehaviour
{
	public static int maxSlots					= 6;
	public Image selectionBox					= null;
	public List<C_InventorySlot> inventorySlots	= new List<C_InventorySlot>(maxSlots);
	public int selectedSlot						= -1;

	void Awake ()
	{
		C_InventorySlot[] children = gameObject.GetComponentsInChildren<C_InventorySlot> ();
		int i = 0;
		foreach(C_InventorySlot child in children)
		{
			inventorySlots[i] = child;
			i++;
			// Currently Inventory can only have 6 slots
			if(i>=maxSlots)
				break;
		}

		if(selectionBox == null)
			Debug.LogWarning ("SelectionBox is missing from InventoryManager");
	}

	void Update()
	{
		if (selectedSlot >= 0)
			selectionBox.transform.position = inventorySlots [selectedSlot].transform.position;
		else {
			selectionBox.transform.position = new Vector3 (-500, 0, 0);
		}
	}

	public C_InventorySlot GetEmptySlot()
	{
		for(int i=0; i<inventorySlots.Count; i++)
		{
			if(inventorySlots[i].itemInSlot == null)
				return inventorySlots[i];
		}
		return null;
	}
	
	public int NumberOfEmptySlots()
	{
		int count = 0;
		for(int i=0; i<inventorySlots.Count; i++)
		{
			if(inventorySlots[i].itemInSlot == null)
				count++;
		}
		return count;
	}

	public int GetItemID(string itemName)
	{
		for(int i=inventorySlots.Count-1; i>-1; i--)
		{
			if(inventorySlots[i].itemInSlot != null)
			{
				if(inventorySlots[i].itemInSlot.itemName == itemName)
					return i;
			}
		}
		return -1;
	}

	public void CombineSlot(int slotID1, int slotID2)
	{
		foreach(C_Combination combo in inventorySlots[slotID1].itemInSlot.combinationList)
		{
			if(combo.itemToCombine == inventorySlots[slotID2].itemInSlot)
			{
				// if have enough empty slots
				if( (NumberOfEmptySlots() + 2) >= combo.resultItems.Count)
				{
//					C_ChapterManager.currentChapterManager.conditionTriggers.Remove ("have_" + inventorySlots[slotID1].itemInSlot.itemName);
//					C_ChapterManager.currentChapterManager.conditionTriggers.Remove ("have_" + inventorySlots[slotID2].itemInSlot.itemName);
//
//					inventorySlots[slotID1].SetItem(null);
//					inventorySlots[slotID2].SetItem(null);
					inventorySlots[slotID1].DeleteItem();
					inventorySlots[slotID2].DeleteItem();
					//GetEmptySlot().SetItem(combo.resultItem);
					foreach(C_Item result in combo.resultItems)
					{
						GetEmptySlot().SetItem(result);
						C_ChapterManager.currentChapter.conditionTriggers.Add ("have_" + result.itemName);
					}

				}
				break;
			}
		}
	}

	public List<C_Item> GetInventory()
	{
		List<C_Item> inventory = new List<C_Item> ();

		foreach(C_InventorySlot slot in inventorySlots)
		{
			inventory.Add(slot.itemInSlot);
		}

		return inventory;
	}
	
}
