using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class C_Item : MonoBehaviour
{
	public string itemName				= "";
	public string itemDescription		= "";
	public int itemID {get;private set;}
	
	// For combinations of items, interactions and get conditions
	// public List<C_Interaction> interactionList = new List<C_Interaction>();
	[SerializeField]
	public List<C_Combination> combinationList = new List<C_Combination>();

	static private int idCounter		= 0;

	// May have problem initialising it with Sprite
	public Sprite itemSprite			= null;

	void Awake () {
		SetID ();
	}

	void SetID()
	{
		itemID = idCounter;
		idCounter++;
	}

}
