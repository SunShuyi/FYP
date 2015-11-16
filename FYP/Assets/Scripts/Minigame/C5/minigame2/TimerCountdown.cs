using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCountdown : MonoBehaviour {

	
	public Slider slider;
	public int speed = 1;
	public float val = 0;
	// Use this for initialization
	void Start () {
		val = slider.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (val > 0)
		{
			val -= Time.deltaTime * speed;
		slider.value = val;
		}
		else
			Reset ();
	}
	
	
	void Reset ()
	{ Application.LoadLevel ("C5_Beach2"); }
	
	
	
}