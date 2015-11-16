using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour {

	public int starting;
	public int StrengthCount;
	
	private Text StrengthText;

	// Use this for initialization
	void Start () {

		StrengthCount = starting;
		
		StrengthText = GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		//StrengthText.text = " " + StrengthCount;
		if (StrengthCount < 0) 
		{ StrengthCount = 0; }
		
		if (StrengthCount > starting) 
		{ StrengthCount = starting; }

	
	}
	void Hit () 
	{ StrengthCount--; }
}
