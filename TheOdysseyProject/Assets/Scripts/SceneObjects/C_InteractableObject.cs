using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_InteractableObject : MonoBehaviour
{
	[HideInInspector]
	public Collider2D clickCollider			= null;
	[HideInInspector]
	public Collider2D interactCollider		= null;

	[Header("Button Interactions")]
	public E_InteractType interactionType	= E_InteractType.None;
	public GameObject buttons				= null;
	public string observeLine				= "";
	public string loadScene					= "";
	
	[Header("Item Interactions")]
	public string wrongItemLine				= "";

	// Conditions and Results list
	[SerializeField]
	public List<C_ConditionResult> conditionResultList = new List<C_ConditionResult>();

	// Use this for initialization
	void Awake () {

		if (this.gameObject.GetComponent<Collider2D> ())
			clickCollider = this.gameObject.GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("ClickCollider is missing from InteractableObject " + gameObject.name);
		
		if(this.gameObject.transform.GetChild(0) == null)
			Debug.LogWarning ("Child gameobject is missing from InteractableObject " + gameObject.name);
		else if (this.transform.GetChild(0).GetComponent<Collider2D> ())
			interactCollider = this.transform.GetChild(0).GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("InteractCollider is missing from InteractableObject " + gameObject.name + "'s child");
		
	}
}
