using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter1 : C_Chapter
{
	#region Singleton Structure
	
	private static C_Chapter1 _instance;
	private C_Chapter1(){}
	
	public static C_Chapter1 getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Chapter1)FindObjectOfType(typeof(C_Chapter1));
				if(_instance == null)
					_instance = new C_Chapter1();
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
		chapterNo = 1;
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

	}
	
	#endregion
	
}
