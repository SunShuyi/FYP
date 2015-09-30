using UnityEngine;
using System.Collections;

public class C_cameraaaa : MonoBehaviour {

	public GameObject theOdysseusMG1 ;
	float dist = 10.0f;
	public Rotate rotation;
	// Update is called once per frame
	void Update () {
		switch (rotation.state) {
		case 0:
		{
			DownOffSet();
			break;
		}
		case 1:
		{
			RightOffSet();
			break;
		}
		case 2:
		{
			UpOffSet();
			break;
		}
		case 3:
		{
			LeftOffSet();
			break;
		}
		default:
		{
			DownOffSet();
			break;
		}
		}
	}

	void DownOffSet()
	{
		Vector3 temp = new Vector3 ();
		temp.x = theOdysseusMG1.transform.position.x;
		temp.y = theOdysseusMG1.transform.position.y ;
		temp.z = theOdysseusMG1.transform.position.z - dist;
		transform.position = temp;
	}

	void UpOffSet()
	{
		Vector3 temp = new Vector3 ();
		temp.x = theOdysseusMG1.transform.position.x;
		temp.y = theOdysseusMG1.transform.position.y ;
		temp.z = theOdysseusMG1.transform.position.z - dist;
		transform.position = temp;
	}

	void LeftOffSet()
	{
		Vector3 temp = new Vector3 ();
		temp.x = theOdysseusMG1.transform.position.x ;
		temp.y = theOdysseusMG1.transform.position.y;
		temp.z = theOdysseusMG1.transform.position.z - dist;
		transform.position = temp;
	}

	void RightOffSet()
	{
		Vector3 temp = new Vector3 ();
		temp.x = theOdysseusMG1.transform.position.x ;
		temp.y = theOdysseusMG1.transform.position.y;
		temp.z = theOdysseusMG1.transform.position.z - dist;
		transform.position = temp;
	}
}
