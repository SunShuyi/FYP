using UnityEngine;
using System.Collections;

public class Detect: MonoBehaviour {
	
	public EmemyWaypoint wp;
	// Use this for initialization
	void Start () {
		wp = transform.parent.GetComponent<EmemyWaypoint> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player")) {
			wp.Chase = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player")) {
			wp.Chase = false;
		}
	}
	
	
	
}
