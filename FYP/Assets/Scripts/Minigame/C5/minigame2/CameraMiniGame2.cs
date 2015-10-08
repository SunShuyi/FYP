using UnityEngine;
using System.Collections;

public class CameraMiniGame2 : MonoBehaviour {

	public GameObject theShip;
	float dist = 10.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3();
		//temp.x = theBoat.transform.position.x;
		temp.y = theShip.transform.position.y + 3.0f;
		temp.z = theShip.transform.position.z - dist;
		transform.position = temp;
	}
}
