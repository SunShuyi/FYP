using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {
	
	//public static int timers;
	public Text text;
	public  int time;
	private float rtime;
	public Image map;
	public GameObject theMap;
	private int seconds = 5;

	// Use this for initialization
	void Start () 
	{
		text.enabled = true;
		theMap = GameObject.Find ("Mini-Map-Icon-Opened");

		time = 5;
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
				seconds =(time*1);
				text.text = seconds.ToString();
				if(seconds <= 0)
				{
					this.enabled = false;
					text.enabled = false;
				}
		
				
			}
		}
		if (time<= 0) {
			map.enabled = false;

		}
	}

}