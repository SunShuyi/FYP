using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C_LevelSelectionManager : MonoBehaviour
{
	#region Member variables
	
	public Sprite blank					= null;
	public Animator animator			= null;
	public Text titleText				= null;
	public Text detailsText				= null;
	public Image detailImage			= null;

	public List<string> chapterTitles	= new List<string> (5);
	public List<string> chapterDetails	= new List<string> (5);
	public List<string> chapterDetailsDutch	= new List<string> (5);
	public List<Sprite> chapterSprites	= new List<Sprite> (5);

	#endregion

	#region Event Trigger Functions

	public void PointerEnterChapter(int chapterNo)
	{
		titleText.text = chapterTitles [chapterNo-1];
		if (PlayerPrefs.GetInt ("Language") == 1) {
			detailsText.text = chapterDetails [chapterNo - 1];
		} else if (PlayerPrefs.GetInt ("Language") == 2) {
			detailsText.text = chapterDetailsDutch [chapterNo - 1];
		}
		detailImage.sprite = chapterSprites [chapterNo-1];

		animator.SetBool ("showDetails",true);
	}
	
	public void PointerExitChapter()
	{
		titleText.text = "";
		detailsText.text = "";
		detailImage.sprite = blank;
		
		animator.SetBool ("showDetails",false);
	}

	public void ClickedChapter(int chapterNo)
	{
		switch(chapterNo)
		{
		case 1:
			Application.LoadLevel("C1_ShipDeck");
			break;
		case 2:
			if(C_Chapter2.getInstance.conditionTriggers.Contains("odysseus_drankWine"))
				Application.LoadLevel("C2_CastleInterior");
			else
				Application.LoadLevel("C2_ShipDeck");
			break;
		case 3:
			if(C_Chapter3.getInstance.conditionTriggers.Contains("done_ritual"))
				Application.LoadLevel("C3_Trench");
			else
				Application.LoadLevel("C3_ShipDeck");
			break;
		case 4:
			if(C_Chapter4.getInstance.conditionTriggers.Contains("shipAtCave"))
				Application.LoadLevel("C4_ShipDeckAfterMG");
			else
			{
				if(C_Chapter4.getInstance.conditionTriggers.Contains("startedGame"))
					Application.LoadLevel("C4_ShipDeck");
				else
				{
					C_Chapter4.getInstance.conditionTriggers.Add ("startedGame");
					Application.LoadLevel("C4_Cutscenes_Opening");
				}
			}
			break;
		case 5:
			// Application.LoadLevel("C5_???");
			//Application.LoadLevel("C5_Phaeacians'Forest1");
			if(C_Chapter5.getInstance.conditionTriggers.Contains("GottenWM"))
				Application.LoadLevel("C5_EumaeusKitchen");
			else
			{
				if(C_Chapter5.getInstance.conditionTriggers.Contains("startedGame"))
					Application.LoadLevel("C5_Phaeacians'Forest1");
				else
				{
					C_Chapter5.getInstance.conditionTriggers.Add ("startedGame");
					Application.LoadLevel("C5_Cutscenes_Opening");
				}
			}
			break;
		default:
			break;
		}
	}

	public void BackButton()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void ClickedMinigame(string minigame)
	{
		Application.LoadLevel (minigame);
	}

	#endregion

}
