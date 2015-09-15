using UnityEngine;
using System.Collections;

public class C_Scylla : MonoBehaviour {

	C_Head theHead;

	BoxCollider2D detectRadius;

	Animator scyllaAnim;

	public bool toAttack;

	public bool isRising;
	public bool isAttacking;
	public bool isRetracting;
	public bool splash;

	// Use this for initialization
	void Start () {
		theHead = GetComponent<C_Head> ();
		theHead.enabled = false;

		detectRadius = gameObject.GetComponent<BoxCollider2D> ();
		detectRadius.enabled = true;

		scyllaAnim = gameObject.GetComponentInChildren<Animator>();

		toAttack = false;
		
		isRising = false;
		isAttacking = false;
		isRetracting = false;
		splash = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (toAttack == true) 
		{ theHead.enabled = true; }

		if(theHead.enabled == true && theHead.atkDelay <= 0 && isRising == true)
		{ isAttacking = true; }

		if(theHead.enabled == true && theHead.atkTime <= 0 &&  isAttacking == true)
		{ splash = true; }

		if (theHead.enabled == true && theHead.atkDuration <= 0 && isAttacking == true) 
		{ isRetracting = true; }

		Animation ();
	}

	void Animation ()
	{
		if (isRising) {
			scyllaAnim.SetBool("isRising", true);
			scyllaAnim.SetBool("isAttacking", false);
			scyllaAnim.SetBool("isRetracting", false);
		}

		if (isAttacking) {
			scyllaAnim.SetBool("isRising", false);		
			scyllaAnim.SetBool("isAttacking", true);			
			scyllaAnim.SetBool("isRetracting", false);
		}

		if (isRetracting) {
			scyllaAnim.SetBool("isRising", false);		
			scyllaAnim.SetBool("isAttacking", false);
			scyllaAnim.SetBool("isRetracting", true);
		}
	}


	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.CompareTag ("Player")) {
			isRising = true;
			toAttack = true;
			detectRadius.enabled = false;
		}
	}
	
}
