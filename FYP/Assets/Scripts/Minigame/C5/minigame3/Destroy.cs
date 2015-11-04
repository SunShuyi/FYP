using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public bool byTime = false;
	public bool byContact = false;
	public bool byVelocity = false;
	public Rigidbody rgBody;
	public float destroyTime;

	// Use this for initialization
	void Start () {
		if(byTime)
		Destroy (this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (byVelocity) {
			if (rgBody.velocity == new Vector3 (0, 0,0))
				Destroy (this.gameObject);
		}
	}


	void OnTriggerEnter2D()
	{
		if(byContact)
		Destroy (this.gameObject);
	}
}
