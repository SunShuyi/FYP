using UnityEngine;
using System.Collections;

public class C_Whirlpool : MonoBehaviour {

	public float whirlpoolForce;
	bool isActive;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.CompareTag ("Player")) {
		//	coll.attachedRigidbody.AddForce(Vector2.MoveTowards(coll.gameObject.transform.position, 
		//	                                                    this.transform.position, 
		//	                                                    whirlpoolForce * Time.deltaTime));

			coll.attachedRigidbody.AddForce(new Vector2(-1,-1) * whirlpoolForce * Time.deltaTime);

		//	coll.attachedRigidbody.AddForce(Vector3.down * whirlpoolForce * Time.deltaTime);
			coll.attachedRigidbody.drag = 10.0f;
		}
	}
}
