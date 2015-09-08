using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_HadesGateManager : MonoBehaviour
{
	public float winTimer						= 2.0f;
	public string nextScene						= "";
	
	public GameObject instructionsImg			= null;
	public Sprite buttonOff						= null;
	public Sprite buttonOn						= null;
	public Sprite buttonLocked					= null;

	public List<C_C3_MG1_Buttons> winButtons	= new List<C_C3_MG1_Buttons>();
	
	public static C_HadesGateManager instance	= null;

	private bool _showInstructions				= true;

	void Start()
	{
		instance = this;
	}

	void Update()
	{
		if(_showInstructions)
		{
			C_Input.getInstance.InputUpdate();

			if(C_Input.getInstance.I_Up || Input.GetKeyUp(KeyCode.Space))
			{
				Destroy(instructionsImg);
				_showInstructions = false;
			}

			return;
		}

		bool winCondition = true;
		foreach(C_C3_MG1_Buttons button in winButtons)
		{
			if(button.toggledOn)
			{
				winCondition = false;
				break;
			}
		}

		if (winCondition)
		{
			StartCoroutine(MoveToNextScene());
		}

	}

	IEnumerator MoveToNextScene()
	{
		yield return new WaitForSeconds(winTimer);

		Application.LoadLevel (nextScene);
	}

}
