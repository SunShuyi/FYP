using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_InventorySlot : MonoBehaviour
{
	//[HideInInspector]
	public C_Item itemInSlot			= null;
	//[HideInInspector]
	// May not need collider already as Button is being used now
	//public BoxCollider2D slotCollider	= new BoxCollider2D();
	//[HideInInspector]
	public Image slotImage				= null;

	void Awake ()
	{
		//slotCollider = gameObject.GetComponent<BoxCollider2D> ();
		slotImage = gameObject.GetComponent<Image> ();
	}

	public void DeleteItem()
	{
		C_ChapterManager.currentChapter.conditionTriggers.Remove ("have_" + itemInSlot.itemName);
		SetItem (null);
	}

	public bool SetItem(C_Item item)
	{
		if(item == null)
		{
			itemInSlot = null;
			//slotImage.sprite = C_ChapterManager.currentChapter.getInstance.blank;
			slotImage.sprite = C_Constants.blank;
			//slotImage.sprite = null;
			return true;
		}

		if(item.itemSprite == null)
			return false;

		itemInSlot = item;
		slotImage.sprite = item.itemSprite;
		return true;
	}

}
