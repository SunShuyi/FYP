using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C_C3_MG1_Buttons : MonoBehaviour
{
	float moveTimeInSec						= 2.0f;
	float speed								= 2.0f;

	public List<C_C3_MG1_Bone> bonesList	= new List<C_C3_MG1_Bone>();

	bool cannotActivate						= false;
	[HideInInspector]
	public bool toggledOn					= true;

	#region OnClick Functions
	
	public void ButtonClick()
	{
		bool con_check = true;
		bool all_moved = false;
		foreach(C_C3_MG1_Bone bone in bonesList)
		{
			if(bone.isMoving)
			{
				con_check = false;
				break;
			}

			if(bone.isMoved)
			{
				if(bonesList.IndexOf(bone) == 0)
				{
					all_moved = true;
				}
				else
				{
					if(!all_moved)
					{
						con_check = false;
						break;
					}
				}
			}
			else
			{
				if(all_moved)
				{
					con_check = false;
					break;
				}
			}

		}

		if (con_check)
		{
			foreach (C_C3_MG1_Bone bone in bonesList) {
				StartCoroutine (MoveBone (bone));
			}
			toggledOn = !toggledOn;
			
			if (toggledOn) {
				gameObject.GetComponent<Image> ().sprite = C_HadesGateManager.instance.buttonOn;
			} else {
				gameObject.GetComponent<Image> ().sprite = C_HadesGateManager.instance.buttonOff;
			}
		}
		else
		{
			StartCoroutine(ButtonLocked());
		}

	}
	
	#endregion

	IEnumerator MoveBone(C_C3_MG1_Bone bone)
	{
		float currMoveTime = 0.0f;
		bone.isMoving = true;

		while(currMoveTime < moveTimeInSec)
		{
			currMoveTime += Time.deltaTime;
			if(bone.blockingSide == E_HorizontalDirection.Left)
			{
				if(bone.isMoved)
				{
					bone.gameObject.transform.SetPositionX(bone.gameObject.transform.position.x + Time.deltaTime*speed);
				}
				else
				{
					bone.gameObject.transform.SetPositionX(bone.gameObject.transform.position.x - Time.deltaTime*speed);
				}
			}
			else
			{
				if(bone.isMoved)
				{
					bone.gameObject.transform.SetPositionX(bone.gameObject.transform.position.x - Time.deltaTime*speed);
				}
				else
				{
					bone.gameObject.transform.SetPositionX(bone.gameObject.transform.position.x + Time.deltaTime*speed);
				}
			}
			
			yield return null;
		}

		bone.isMoved = !bone.isMoved;
		bone.isMoving = false;
	}

	IEnumerator ButtonLocked()
	{
		gameObject.GetComponent<Image> ().sprite = C_HadesGateManager.instance.buttonLocked;

		yield return new WaitForSeconds(0.1f);
		
		if (toggledOn) {
			gameObject.GetComponent<Image> ().sprite = C_HadesGateManager.instance.buttonOn;
		} else {
			gameObject.GetComponent<Image> ().sprite = C_HadesGateManager.instance.buttonOff;
		}
	}

}
