using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class C_CutsceneTxt : MonoBehaviour {

	public string[] TxtArry_Eng = new string[8];
	public string[] TxtArry_Dutch = new string[8];
	private string[] TxtArry_Curr = new string[8];

	public bool EngOrDutch  = true; //True == Eng, False == Dutch;

	public int txtCount;

	CutsceneHandler theCutscene;

	Text theText;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("Language") == 1)
			TxtArry_Curr = TxtArry_Eng;
		else 
			TxtArry_Curr = TxtArry_Dutch;

		txtCount = 0;
		theText = this.GetComponent<Text> ();
	}

	
	// Update is called once per frame
	void Update () 
	{ theText.text = TxtArry_Curr[txtCount]; }
}
