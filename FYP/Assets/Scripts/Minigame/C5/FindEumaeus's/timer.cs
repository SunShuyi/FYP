using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	//public static int timers;
	public  int time;
	private float rtime;
	public Image map;
	public GameObject theMap;
	//Text text;

	// Use this for initialization
	void Start () 
	{
		theMap = GameObject.Find ("Mini-Map-Icon-Opened");

		//text = GetComponent<Text> ();	
		time = 5;
		//guiText.text = "map" + timer;
		rtime = Time.time;
	}

	// Update is called once per frame
	void Update () 
	{
		if(theMap == null)
		{
			if (Time.time - rtime > 1.0f) 
			{
				time -=1;
				rtime = Time.time;
				//text.text = "" + time;
				//guiText.text = "map" + timer;

			}
		}
			if (time<= 0) {
				map.enabled = false;
		}
	}
}