﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Chapter5 : C_Chapter
{
	#region Singleton Structure
	
	private static C_Chapter5 _instance;
	private C_Chapter5(){}
	
	public static C_Chapter5 getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Chapter5)FindObjectOfType(typeof(C_Chapter5));
				if(_instance == null)
					_instance = new C_Chapter5();
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
		chapterNo = 5;
	}
	
	#endregion
	
	#region Saved Datas
	
	public static E_Player s_currentPlayer = E_Player.Odysseus;
	public static string s_lastScene = "";
	public static List<string> s_pickedUpItems = new List<string> ();
	public static List<string> s_conditionTriggers = new List<string> (1){"level_1"};
	public RuntimeAnimatorController nextRAC;
	public Sprite nextSprite;

	public override E_Player currentPlayer { get { return s_currentPlayer; } set{s_currentPlayer = value;}}
	public override string lastScene { get { return s_lastScene; } set{s_lastScene = value;}}
	public override List<string> destroyedObjects { get { return s_pickedUpItems; } set{s_pickedUpItems = value;}}
	public override List<string> conditionTriggers { get { return s_conditionTriggers; } set{s_conditionTriggers = value;}}
	
	#endregion
	
	#region Condition Management

	void Update()
	{
		if (scene == "C5_Phaeacians'Forest1" || scene == "C5_Phaeacians'Beach")
		{
			if(conditionTriggers.Contains ("woreCloak"))
			{
			}
			else if(conditionTriggers.Contains ("have_CanvasCloak")) {
				C_PlayerData tempPlayerData = GetPlayer (currentPlayer);
				tempPlayerData.animationController = nextRAC;
				tempPlayerData.playerSprite = nextSprite;
				C_Player tempPlayer = C_Player.getInstance;
				tempPlayer.ChangePlayer(currentPlayer);
				//conditionTriggers.Remove("have_CanvasCloak");
				conditionTriggers.Add("woreCloak");
				GameObject.Find("C5_Phaeacians'Beach").SetActive(false);
			}
		}
	}
	public override void ConditionCheck()
	{
		if(scene == "C5_Phaeacians'Beach")
		{
			if(conditionTriggers.Contains("DeleteHidden")) //gate_unlocked XML condition
			{
				GameObject.Find("Hidden").SetActive(false); //GateMG at interactive object
						
			}

			C_PlayerData tempPlayer = GetPlayer (currentPlayer);
			tempPlayer.animationController = nextRAC;
			tempPlayer.playerSprite = nextSprite;
			conditionTriggers.Remove("woreCloak");
			//conditionTriggers.Remove("have_CanvasCloak");
		}

		if(scene == "C5_Phaeacians'Forest1")
		{
			if(conditionTriggers.Contains("DeleteHiddenForest")) //gate_unlocked XML condition
			{
				GameObject.Find("Hidden").SetActive(false); //GateMG at interactive object

			}
			if(conditionTriggers.Contains ("have_CanvasCloak")) {
				C_PlayerData tempPlayer = GetPlayer (currentPlayer);
				tempPlayer.animationController = nextRAC;
				tempPlayer.playerSprite = nextSprite;
				//conditionTriggers.Remove("have_CanvasCloak");
				conditionTriggers.Add("woreCloak");
			}
		}

		if(scene == "C5_EumaeusHutLivingRoom")
		{
			if(conditionTriggers.Contains("DeleteHiddenBedroom")) //gate_unlocked XML condition
			{
				GameObject.Find("Hidden").SetActive(false); //GateMG at interactive object
				
			}
		}
		if(scene == "C5_OdysseusHall")
		{
			
			if(conditionTriggers.Contains("DeletePenelopeRoom")) //gate_unlocked XML condition
			{
				GameObject.Find("Penelope'sDoor").SetActive(false); //GateMG at interactive object
				
			}
		}


	}
	#endregion	
}
