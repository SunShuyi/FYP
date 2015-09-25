using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	
	public int time;
	private float rtime;
	public Image map;


	// Use this for initialization
	void Start () {

                time = 5;
				rtime = Time.time;
				
		}
	

	
	// Update is called once per frame
	void Update () {
		

		if (Time.time - rtime > 1.0f) {
			time -=1;
			rtime = Time.time;
	
		}
		if (time<= 0) {

			map.enabled = false;
	}
}



}