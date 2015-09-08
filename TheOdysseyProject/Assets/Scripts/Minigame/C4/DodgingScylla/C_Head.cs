using UnityEngine;
using System.Collections;

public class C_Head : MonoBehaviour {

	public float atkDelay; //time before attacking
	public float atkTime; //time taken to attack
	public float atkDuration; // duration of attack

	public float minAtkTime = 2.0f;
    public float maxAtkTime = 6.0f;

	private bool startAtk;

	CircleCollider2D atkRadius;
	BoxCollider2D detectRadius;

	Animator theBoat;

	// Use this for initialization
	void Start () {
		startAtk = false;
		detectRadius = gameObject.GetComponent<BoxCollider2D> ();
		atkRadius = gameObject.GetComponent<CircleCollider2D> ();
		atkRadius.enabled = false;

		RandomTime ();
	}
	
	// Update is called once per frame
	void Update () {

		if (atkDelay > 0) {
			atkDelay -= Time.deltaTime;
		} else {
			startAtk = true;
		}

			if(detectRadius.enabled == false && atkTime > 0)
			{ atkTime -= Time.deltaTime; }
		//	else
		//	{ atkRadius.enabled = true; Debug.Log ("Strike!"); }

			if(atkRadius.enabled == true && atkDuration > 0)
			{ atkDuration -= Time.deltaTime; } 
			else 
			{ atkRadius.enabled = false; } 
	}

	void RandomTime ()
	{
		atkDelay = Random.Range (minAtkTime, maxAtkTime);
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (startAtk) {
			if (col.CompareTag("Player")) 
			{ //col.gameObject.SendMessage("Reset"); 
				col.attachedRigidbody.velocity = Vector3.zero;
				theBoat = col.gameObject.GetComponent<Animator>();
				theBoat.SetBool("boat_die", true);
			}
		}
	}
}
