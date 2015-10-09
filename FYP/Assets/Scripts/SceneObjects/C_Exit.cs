using UnityEngine;
using System.Collections;

public class C_Exit : MonoBehaviour {

	public string nextScene						= "";
	[Header("Enter direction")]
	public E_HorizontalDirection enterDirection	= E_HorizontalDirection.Right;
	[HideInInspector]
//	[Header("Enter direction Vertical")]
//	public E_VerticalDirection enterVDirection	= E_VerticalDirection.Right;
//	[HideInInspector]

	public Collider2D exitCollider				= null;

	[Space(10)]
	[Header("Lock exit position")]
	public bool lockX					= false;
	public bool lockY					= false;
	[Range(0.0f,2.0f)]
	public float lockValueX				= 0.0f;
	[Range(0.0f,2.0f)]
	public float lockValueY				= 0.0f;

	void Awake()
	{
		if (this.gameObject.GetComponent<Collider2D> ())
			exitCollider = this.gameObject.GetComponent<Collider2D> ();
		else
			Debug.LogWarning ("Collider is missing from Exit " + gameObject.name);
	}

	public Vector3 GetTargetPosition()
	{
		Vector3 targetPos = Camera.main.ScreenToWorldPoint (C_Input.getInstance.I_Up_Position);
		targetPos.z = 0;

		if (lockX) {
			targetPos.x = exitCollider.bounds.min.x + lockValueX * exitCollider.bounds.extents.x;
		}
		if (lockY) {
			targetPos.y = exitCollider.bounds.min.y + lockValueY * exitCollider.bounds.extents.y;
		}

		return targetPos;
	}

}
