using UnityEngine;
using System.Collections;

public class C_cameraaaa : MonoBehaviour {

	public GameObject theOdysseusMG1 ;
	float dist = 10.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3();
		//temp.x = theBoat.transform.position.x;
		temp.y = theOdysseusMG1.transform.position.y + 3.0f;
		temp.z = theOdysseusMG1.transform.position.z - dist;
		transform.position = temp;
	}
}
