﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPage : MonoBehaviour {
	//public Text text;
	public  int time;
	private float rtime;
	//public Image Win;
	private int seconds = 1;

	// Use this for initialization
	void Start () {
		//text.enabled = true;
		time = 1;
		rtime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - rtime > 1.0f) 
		{
			time -=1;
			rtime = Time.time;
			seconds =(time*1);
			//text.text = seconds.ToString();
			if(seconds <= 0)
			{
				this.enabled = false;
				//text.enabled = false;
			}
			
			
		}
		if (time<= 0) {
			//Win.enabled = false;
			Application.LoadLevel ("C5_Cutscenes_Victory");
			
		}
	}
	
	}

