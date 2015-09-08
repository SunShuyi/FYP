using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter2 : C_Chapter
{
	#region Singleton Structure
	
	private static C_Chapter2 _instance;
	private C_Chapter2(){}
	
	public static C_Chapter2 getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Chapter2)FindObjectOfType(typeof(C_Chapter2));
				if(_instance == null)
					_instance = new C_Chapter2();
			}
			return _instance;
		}
	}
	
	#endregion
	
	#region Override Functions for C_Chapter
	
	public override C_Chapter Instance ()
	{
		return getInstance;
	}
	
	public override void Awake ()
	{
		base.Awake ();
		chapterNo = 2;
	}
	
	public override void DeleteChapter ()
	{
		base.DeleteChapter ();
		
		_instance = null;
		// RESET STATIC VALUES
		s_currentPlayer = E_Player.Odysseus;
		s_lastScene = "";
		s_pickedUpItems = new List<string> ();
		// Starting level?
		s_conditionTriggers = new List<string> (1){"level_1"};
	}
	
	#endregion
	
	#region Saved Datas
	
	public static E_Player s_currentPlayer = E_Player.Odysseus;
	public static string s_lastScene = "";
	public static List<string> s_pickedUpItems = new List<string> ();
	public static List<string> s_conditionTriggers = new List<string> (1){"level_1"};
	
	public override E_Player currentPlayer { get { return s_currentPlayer; } set{s_currentPlayer = value;}}
	public override string lastScene { get { return s_lastScene; } set{s_lastScene = value;}}
	public override List<string> destroyedObjects { get { return s_pickedUpItems; } set{s_pickedUpItems = value;}}
	public override List<string> conditionTriggers { get { return s_conditionTriggers; } set{s_conditionTriggers = value;}}
	
	#endregion
	
	#region Condition Management
	
	public override void ConditionCheck()
	{
		if (conditionTriggers.Contains ("level_1")) {
			if (scene == "C2_Clearing") {
				if (destroyedObjects.Contains ("Deer")) {
					foreach (GameObject go in sceneManager.inactiveGameobjects) {
						if (go.name == "Deer_Dead") {
							go.SetActive (true);
						}
					}
				}
			}
		} else {
			
			if (scene == "C2_Clearing")
			{
				foreach (GameObject go in sceneManager.inactiveGameobjects)
				{
					if (go.name == "Castle")
					{
						go.SetActive (true);
					}
				}
			}
			else if (scene == "C2_Cliff")
			{
				if (destroyedObjects.Contains ("Beehive"))
				{
					foreach (GameObject go in sceneManager.inactiveGameobjects)
					{
						if (go.name == "Beehive_Broken")
						{
							go.SetActive (true);
						}
					}
				}
			}
		}

		if (conditionTriggers.Contains("level_2"))
		{
			if(scene == "C2_OdysseusRoom")
			{
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "Odysseus")
					{
						go.SetActive(true);
					}
				}
			}
			else if (scene == "C2_ShipDeck")
			{
				GameObject.Find("Eurylochus").SetActive(false);
			}
		}
		
		else if (conditionTriggers.Contains ("level_3"))
		{
			if(scene == "C2_Clearing")
			{
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "Hermes")
					{
						go.SetActive(true);
					}
				}
			}
			else if(scene == "C2_CastleExterior")
			{
				GameObject.Find("Circe").SetActive(false);

				if (conditionTriggers.Contains ("Lion_gotMeat") && conditionTriggers.Contains ("Wolf_gotMeat"))
				{
					foreach(GameObject go in sceneManager.inactiveGameobjects)
					{
						if(go.name == "Castle")
						{
							go.SetActive(true);
						}
					}
				}
				else
				{
					foreach(GameObject go in sceneManager.inactiveGameobjects)
					{
						if(go.name == "Door")
						{
							go.SetActive(true);
						}
					}
				}
			}
			else if(scene == "C2_CastleInterior")
			{
				if (conditionTriggers.Contains ("odysseus_drankWine"))
				{
					GameObject.Find("Castle").SetActive(false);
				}
			}
		}

	}
	
	#endregion
	
}
