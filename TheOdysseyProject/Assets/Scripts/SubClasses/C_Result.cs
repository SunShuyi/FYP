using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class C_Result // : MonoBehaviour
{
	[Header("General settings")]
	public E_ResultType resultType				= E_ResultType.ShowLine;
	public bool haveBuffer						= false;
	public float bufferTime						= 0.0f;		// in seconds
	
	[Header("Show One-line sentence")]
	public string theSentence					= "";
	[Header("Animation change")]
	public string animationBool					= "";
	[Header("Obtain item")]
	public C_Item obtainItem					= null;
	[Header("Delete item")]
	public string deleteItem					= null;
	[Header("GameObject results")]
	public GameObject destroyGameObject			= null;
	public GameObject setGameObjectActive		= null;
	[Header("Add/Remove Condition")]
	public string condition						= "";

}
