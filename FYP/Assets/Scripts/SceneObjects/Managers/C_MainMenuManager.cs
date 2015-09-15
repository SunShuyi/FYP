using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_MainMenuManager : MonoBehaviour
{
	static bool gameStart = false;
	
	public List<C_Chapter>gameData = new List<C_Chapter>();
	
	void Awake()
	{
		if (!gameStart) {
			
			if(gameData.Count > 0)
			{
				foreach(C_Chapter chapter in gameData)
				{
					foreach(C_PlayerData data in chapter.playerData)
					{
						data.ResetInventory();
					}
				}
			}
			else
				Debug.LogWarning("No Game Data to reset?\nSet datas in MainMenuManager");
			
			gameStart = true;
		}
	}

	#region OnClick Functions

	// Can actually combine all OnClick functions into one

	public void StartButton()
	{
		// Load level selection scene

		Application.LoadLevel ("LevelSelection");
	}
	
	public void OptionsButton()
	{
		// Load options scene
		
		Application.LoadLevel ("Options");
	}
	
	public void ExitButton()
	{
		// Exit application
		
		Application.Quit ();
	}

	#endregion

}
