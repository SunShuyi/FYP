﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

	public int startingLives;
	public int lifeCount;
	
	private Text lifeText;
	
	public static Lives life;
	// Use this for initialization
	void Start () {
		life = this;
		lifeCount = startingLives;
		
		lifeText = GetComponent<Text> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeText.text = " x " + lifeCount;
		
		if (lifeCount < 0) 
		{ lifeCount = 0;
		}
		
		if (lifeCount > startingLives) 
		{ lifeCount = startingLives; 

		}
	}
	
	void Hit () 
	{ lifeCount--; }
}