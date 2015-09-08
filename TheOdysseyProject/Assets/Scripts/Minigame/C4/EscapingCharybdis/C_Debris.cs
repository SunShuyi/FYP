using UnityEngine;
using System.Collections;

public class C_Debris: MonoBehaviour {

	public GameObject theWhirlpool;

	Vector3 whirlpoolAxis;

	public float kbSpeed;
	public float rotateSpeed;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		transform.RotateAround(whirlpoolAxis, Vector3.forward, -rotateSpeed * Time.deltaTime); 
		transform.rotation = Quaternion.LookRotation (Camera.main.transform.forward);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player")) {
			//coll.attachedRigidbody.AddForce(Vector3.down * kbSpeed);
			coll.attachedRigidbody.AddForce(new Vector2(-1,-1) * kbSpeed);
			coll.attachedRigidbody.drag = 4.0f;
		}
	}
}
