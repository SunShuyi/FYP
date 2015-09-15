using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Character : MonoBehaviour
{
	[HideInInspector]
	public Collider2D clickCollider				= null;
	[HideInInspector]
	public Collider2D interactCollider			= null;
	
	// the prefab containing the name, sprite and animator
	public C_DialogueGraphic dialogueGraphic	= null;

	// list of items NPC have (to give player during dialogue)
	public List<C_Item> npcInventory			= new List<C_Item>();

	// Use this for initialization
	void Awake () {

		if (this.gameObject.GetComponent<Collider2D> ())
			clickCollider = this.gameObject.GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("ClickCollider is missing from Character " + gameObject.name);
		
		if(this.gameObject.transform.GetChild(0) == null)
			Debug.LogWarning ("Child gameobject is missing from Character " + gameObject.name);
		else if (this.transform.GetChild(0).GetComponent<Collider2D> ())
			interactCollider = this.transform.GetChild(0).GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("InteractCollider is missing from Character " + gameObject.name + "'s child");

	}


}
