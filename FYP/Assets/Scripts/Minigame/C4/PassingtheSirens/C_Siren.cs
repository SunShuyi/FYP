using UnityEngine;
using System.Collections;

public class C_Siren : MonoBehaviour {

	public GameObject theBoat;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempPos = new Vector3();
		tempPos.x = this.transform.position.x;

		if (transform.position.y < 70) {
			tempPos.y = theBoat.transform.position.y + 1.0f;
			transform.position = tempPos;
		} else {
			transform.Translate(Vector3.up * Time.deltaTime * 5.0f);
			transform.Rotate(Vector3.forward * 0.3f);
		}
	}
}
