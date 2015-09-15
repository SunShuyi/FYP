using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_InWorldItem : MonoBehaviour
{
	[HideInInspector]
	public Collider2D clickCollider		= null;
	[HideInInspector]
	public Collider2D interactCollider	= null;
	
	public bool destroyableObject		= true;

	public GameObject buttonsAccessible	= null;
	public GameObject buttonsLocked		= null;
	[HideInInspector]
	public GameObject buttonsCurrent	= null;

	public E_Player accessiblePlayer	= E_Player.Everyone;

	[SerializeField]
	public C_Item item = null;

	// Use this for initialization
	void Awake () {

		if (this.gameObject.GetComponent<Collider2D> ())
			clickCollider = this.gameObject.GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("ClickCollider is missing from InWorldItem " + gameObject.name);

		if(this.gameObject.transform.GetChild(0) == null)
			Debug.LogWarning ("Child gameobject is missing from InWorldItem " + gameObject.name);
		else if (this.transform.GetChild(0).GetComponent<Collider2D> ())
			interactCollider = this.transform.GetChild(0).GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("InteractCollider is missing from InWorldItem " + gameObject.name + "'s child");

		buttonsCurrent = buttonsAccessible;
	}

	void Update()
	{
		if(C_ChapterManager.currentChapter)
		{
			if(C_ChapterManager.currentChapter.currentPlayer == accessiblePlayer || accessiblePlayer == E_Player.Everyone)
				buttonsCurrent = buttonsAccessible;
			else
				buttonsCurrent = buttonsLocked;
		}
	}

}
