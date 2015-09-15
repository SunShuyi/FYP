using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter3 : C_Chapter
{
	#region Singleton Structure
	
	private static C_Chapter3 _instance;
	private C_Chapter3(){}
	
	public static C_Chapter3 getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Chapter3)FindObjectOfType(typeof(C_Chapter3));
				if(_instance == null)
					_instance = new C_Chapter3();
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
		chapterNo = 3;
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
		if(scene == "C3_GateEntrance")
		{
			if(conditionTriggers.Contains("gate_unlocked"))
			{
				GameObject.Find("GateMG").SetActive(false);

				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "Gate")
					{
						go.SetActive(true);
						break;
					}
				}
			}
		}
		else if(scene == "C3_EndOfDock")
		{
			if(conditionTriggers.Contains("reaper_paid"))
			{
				GameObject.Find("GateBlocked").SetActive(false);
				
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "Gate")
					{
						go.SetActive(true);
						break;
					}
				}
			}
		}
		else if(scene == "C3_Trench")
		{
			if(conditionTriggers.Contains("done_ritual"))
			{
				GameObject.Find("Gate").SetActive(false);
				
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "GateLocked")
					{
						go.SetActive(true);
						break;
					}
				}
			}
		}
		else if(scene == "C3_CerberusCave")
		{
			if(conditionTriggers.Contains("cerberus_sleep"))
			{
				//GameObject.Find("CerberusAwake").SetActive(false);
				
				foreach(GameObject go in sceneManager.inactiveGameobjects)
				{
					if(go.name == "CerberusAsleep")
					{
						go.SetActive(true);
						break;
					}
				}
			}
		}
		else if(scene == "C3_ProphetArea")
		{
			if(conditionTriggers.Contains("firecave_passed"))
			{
				GameObject.Find("FieryCave").SetActive(false);
				
//				foreach(GameObject go in sceneManager.inactiveGameobjects)
//				{
//					if(go.name == "River")
//					{
//						go.SetActive(true);
//						break;
//					}
//				}
			}
		}
	}

	#endregion
	
}
