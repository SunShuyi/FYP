﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	//public static int timers;
	public  int time;
	private float rtime;
	public Image map;
	public GameObject theMap;
	private int seconds = 5;


	// Use this for initialization
	void Start () 
	{
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
		

			}
		}
			if (time<= 0) {
				map.enabled = false;
			    //Time.timeScale =0;
			  
		}
	}

	void OnGUI()
	{ 

		GUI.Label (new Rect(400, 350, 40, 50),""+ seconds.ToString()+ " secs");
		
	}

}