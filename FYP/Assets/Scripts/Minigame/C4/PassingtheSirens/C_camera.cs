using UnityEngine;
using System.Collections;

public class C_camera : MonoBehaviour {

	public GameObject theBoat;
	float dist = 10.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3();
		//temp.x = theBoat.transform.position.x;
		temp.y = theBoat.transform.position.y + 3.0f;
		temp.z = theBoat.transform.position.z - dist;
		transform.position = temp;
	}
}
