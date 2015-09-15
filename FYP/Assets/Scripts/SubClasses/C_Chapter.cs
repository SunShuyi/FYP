using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter : MonoBehaviour
{
	
	#region Member Variables
	
	public int chapterNo	= 0;
	public string scene		= "";
	
	// Set this as the starting character
//	public static E_Player currentPlayer = E_Player.Odysseus;
//	public static string lastScene = "";
//	
//	// problem with this method is if player picks up items of same name (eg. Arrow)
//	// unless each arrow is unique (eg. Arrow1, Arrow2)
//	public static List<string> pickedUpItems = new List<string> ();
//	
//	public static List<string> conditionTriggers = new List<string> (1){"level_1"};
	
	// Set this as the starting character
	public virtual E_Player currentPlayer { get { return E_Player.Odysseus; } set{currentPlayer = value;}}
	public virtual string lastScene { get { return ""; } set{lastScene = value;}}
	
	// problem with this method is if player picks up items of same name (eg. Arrow)
	// unless each arrow is unique (eg. Arrow1, Arrow2)
	public virtual List<string> destroyedObjects { get { return new List<string> (); } set{destroyedObjects = value;}}
	
	// Maybe can add another list for objects to activate
	//public virtual List<string> activatedObjects { get { return new List<string> (); } set{destroyedObjects = value;}}

	public virtual List<string> conditionTriggers { get { return new List<string> (1){"level_1"}; } set{conditionTriggers = value;}}
	
	public List<C_PlayerData> playerData = new List<C_PlayerData> ();
	
	[HideInInspector]
	public C_SceneManager sceneManager = null;

	#endregion

	#region Virtual Methods

	public virtual C_Chapter Instance(){ return null; }

	public virtual void Awake()
	{		
		if (playerData.Count < 1)
			Debug.LogWarning ("NO PLAYABLE CHARACTERS TO LOAD");

		scene = Application.loadedLevelName;
		sceneManager = (C_SceneManager)FindObjectOfType(typeof(C_SceneManager));

		ConditionCheck ();
	}
	
	public virtual void OnDestroy() {
		// only delete when player does not load level
		if (!Application.isLoadingLevel) {
			foreach (C_PlayerData data in playerData) {
				data.ResetInventory ();
			}
		}
	}

	public virtual void DeleteChapter()
	{
		foreach (C_PlayerData data in playerData) {
			data.ResetInventory ();
		}

		currentPlayer = E_Player.Odysseus;
		lastScene = "";
		destroyedObjects = new List<string> ();
		// Starting level?
		conditionTriggers = new List<string> (1){"level_5"};
	}

	public virtual void ConditionCheck(){}
	
	#endregion

	#region Player Management
	
	public bool AddPlayer(E_Player player, List<C_Item> inventory)
	{
		foreach(C_PlayerData data in playerData)
		{
			if(data.player == player)
				return false;
		}
		
		C_PlayerData newData = new C_PlayerData (player,inventory);
		playerData.Add (newData);
		return true;
		
	}
	
	public bool SavePlayer(E_Player player, List<C_Item> inventory)
	{
		foreach(C_PlayerData data in playerData)
		{
			if(data.player == player)
			{
				data.inventory = inventory;
				return true;
			}
		}
		
		return false;
	}
	
	public List<C_Item> GetPlayerInventory(E_Player player)
	{
		foreach(C_PlayerData data in playerData)
		{
			if(data.player == player)
			{
				if(data.inventory.Count != 0)
					return data.inventory;
			}
		}
		
		return null;
	}
	
	public C_PlayerData GetPlayer(E_Player player)
	{
		foreach(C_PlayerData data in playerData)
		{
			if(data.player == player)
			{
				return data;
			}
		}
		return null;
	}

	#endregion

}
