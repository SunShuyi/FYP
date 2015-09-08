using UnityEngine;
using System.Collections;

// =====================================================
//			MAY CHANGE THIS INTO A NAMESPACE
// =====================================================

#region Public Enums

// For C_Timer Class
public enum E_TimerType
{
	Stopwatch,
	Countdown
}

// For C_DialogueNode
public enum E_DialogueType
{
	Chapter,
	Scene,
	NPC,
	Line,
	Condition,
	Question,
	Option,
	Obtain,
	BackToQuestion,
	ContinueDialogue,
	LeaveDialogue,
	ChangeCharacter,
	Result
}

// For C_Player and C_MovingBackground
public enum E_HorizontalDirection
{
	Right,
	Left,
	None
}

public enum E_VerticalDirection
{
	Up,
	Down,
	None
}

// For C_Chapter4Manager
public enum E_Player
{
	None,
	Odysseus,
	Test,
	Eurylochus,
	Everyone
}

public enum E_ResultType
{
	ShowLine,
	ChangeAnimation,
	GiveItem,
	DeleteObject,
	SetObjectActive,
	DeleteItem,
	AddRemoveCondition
}

public enum E_InteractType
{
	None,
	Monologue,
	LoadScene
}

#endregion

public static class C_Constants
{
	
#region DIALOGUE ELEMENT NAMES
	
	public const string D_E_DIALOGUE		= "dialogue";
	public const string D_E_CHAPTER			= "chapter";
	public const string D_E_SCENE			= "scene";
	public const string D_E_NPC				= "npc";
	public const string D_E_LINE			= "line";
	public const string D_E_CONDITION		= "condition";
	public const string D_E_QUESTION		= "question";
	public const string D_E_OPTION			= "option";
	public const string D_E_OBTAIN			= "obtain";
	public const string D_E_BACKTOQN		= "backtoquestion";
	public const string D_E_CONDIALOG		= "continuedialogue";
	public const string D_E_LEAVEDIALOG		= "leavedialogue";
	public const string D_E_CHANGECHARACTER	= "changecharacter";
	public const string D_E_RESULT			= "result";
	
#endregion
	
#region DIALOGUE ATTRIBUTE NAMES
	
	public const string D_A_NAME			= "_name";
	public const string D_A_CONDITION		= "_condition";
	public const string D_A_QUESTION		= "_question";
	public const string D_A_SENTENCE		= "_sentence";
	public const string D_A_CHOICES			= "_choices";
	
#endregion
	
#region DIALOGUE ATTRIBUTE VALUES
	
	public const string D_V_SETACTIVE		= "setactive";
	public const string D_V_CONDITION		= "condition";
	public const string D_V_CHANGEPLAYER	= "changeplayer";
	public const string D_V_DELETEINVENTORY	= "deleteinventory";
	public const string D_V_ADDINVENTORY	= "addinventory";
	public const string D_V_LOADSCENE		= "loadscene";
	public const string D_V_ANIMATIONBOOL	= "animationbool";
	public const string D_V_ENDCHAPTER		= "endchapter";
	
#endregion

	public static Sprite blank
	{
		get{
			// Will constantly call Load function tho ...
			return Resources.Load <Sprite>("blank");
		}
	}

}
