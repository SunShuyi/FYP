using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter4 : C_Chapter
{
	#region Singleton Structure

	private static C_Chapter4 _instance;
	private C_Chapter4(){}
	
	public static C_Chapter4 getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Chapter4)FindObjectOfType(typeof(C_Chapter4));
				if(_instance == null)
					_instance = new C_Chapter4();
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
		chapterNo = 4;
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
		// Demo
		//s_conditionTriggers = new List<string> (2){"shipAtCave", "level_5"};
	}

	#endregion

	#region Saved Datas

	// Set this as the starting character
	public static E_Player s_currentPlayer = E_Player.Odysseus;
	public static string s_lastScene = "";
	public static List<string> s_pickedUpItems = new List<string> ();
	// Starting level?
	public static List<string> s_conditionTriggers = new List<string> (1){"level_1"};
	// Demo
	//public static List<string> s_conditionTriggers = new List<string> (2){"shipAtCave", "level_5"};

	public override E_Player currentPlayer { get { return s_currentPlayer; } set{s_currentPlayer = value;}}
	public override string lastScene { get { return s_lastScene; } set{s_lastScene = value;}}
	public override List<string> destroyedObjects { get { return s_pickedUpItems; } set{s_pickedUpItems = value;}}
	public override List<string> conditionTriggers { get { return s_conditionTriggers; } set{s_conditionTriggers = value;}}

	#endregion

	#region Condition Management

	// Hardcoded values, if possible, can try to softcode
	// Can use the "list of activatedObjects" idea
	// But for other stuff, like animation/sprite change, will be different
	public override void ConditionCheck()
	{
		if(scene == "C4_GrassPlains1")
		{
			if(destroyedObjects.Contains("TreeBranch2"))
			{
				// Cannot find an inactive object!!
				// So use a list of inactiveObjects instead
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "TreeBranch3")
					{
						go.SetActive(true);
					}
				}
			}
			else if(destroyedObjects.Contains("TreeBranch1"))
			{
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "TreeBranch2")
					{
						go.SetActive(true);
					}
				}
			}
		}

		if(conditionTriggers.Contains("level_2") || conditionTriggers.Contains("level_3"))
		{
			if(scene == "C4_ShipDeck")
			{
				GameObject.Find("Eurylochus").SetActive(false);
			}

			if(scene == "C4_ShipDeck2")
			{
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "Odysseus")
					{
						go.SetActive(true);
					}
				}

				if(conditionTriggers.Contains("odysseus_bound"))
					GameObject.Find("Odysseus").GetComponent<Animator>().SetBool("isInteracting",true);
				else
					GameObject.Find("Odysseus").GetComponent<Animator>().SetBool("isInteracting",false);
			}
		}

		if (conditionTriggers.Contains ("level_4"))
		{
			if (scene == "C4_ShipDeck")
			{
				if(!conditionTriggers.Contains("player_gotArmor"))
				{
					if(destroyedObjects.Contains("Helmet") && destroyedObjects.Contains("Gauntlets")
					   && destroyedObjects.Contains("Chestplate") && destroyedObjects.Contains("Greaves"))
					{
						conditionTriggers.Add("player_gotArmor");
					}
				}
			}
		}
		
		if (conditionTriggers.Contains ("odysseus_sleeping"))
		{
			if (scene == "C4_ShipDeckAfterMG")
			{
				GameObject.Find ("Eurylochus").SetActive (false);
			}
			
			else if (scene == "C4_Temple")
			{
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "OdysseusSleeping")
					{
						go.SetActive(true);
					}
				}
			}
		}
	}

	#endregion

}
