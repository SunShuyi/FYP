using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class C_ConditionResult // : MonoBehaviour
{
	// List of conditions to trigger results
	public List<string> conditionsList	= new List<string>();

	// List of results
	[SerializeField]
	public List<C_Result> resultsList	= new List<C_Result>();

}
