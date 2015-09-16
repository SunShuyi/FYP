using UnityEngine;
using UnityEngine;
using System.Collections;

public class Porter : MonoBehaviour {
	public GameObject Target_1;
	//public GameObject Target_2;
	
	//check name of what is collided with to determine where the player teleports to.
	//also can check its tag instead of name.
	void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.name)
		{
		case "PorterA":
			gameObject.transform.position = Target_1.transform.position;
			break;
			
//		case "PorterB":
//			gameObject.transform.position = Target_2.transform.position;
//			break;
		}
	}
	void Start () {
	}
	
	void Update () {
	}
}