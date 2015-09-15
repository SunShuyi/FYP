using UnityEngine;
using System.Collections;

public class C_Camera02 : MonoBehaviour {

	public GameObject theBoat;
	float dist = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 temp = new Vector3();
		temp.x = theBoat.transform.position.x + 3.0f;
		temp.y = theBoat.transform.position.y;
		temp.z = theBoat.transform.position.z - dist;
		transform.position = temp;
	
	}
}
