using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCountdown : MonoBehaviour {

	
	public Slider slider;
	public int speed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (slider.value > 0)
			slider.value -= Time.deltaTime*speed;
		else
			Reset ();
	}
	
	
	void Reset ()
	{ Application.LoadLevel ("C5_Beach2"); }
	
	
	
}