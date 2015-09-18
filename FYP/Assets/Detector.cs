using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {

	public wayPoint wp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player"))
			wp.patrolling = false;
			//wp.Combat = true;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player"))
			wp.patrolling = true;
			//wp.Combat = false;
	}



}
