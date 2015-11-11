using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minigame2Timer : MonoBehaviour {
	public Shooter shooter;

	//public static int timers;
	public Text text;
	public  int time;
	private float rtime;
	private int seconds = 10;
	public Slider progressBar;

	public static Minigame2Timer Timer;
	// Use this for initialization
	void Start () 
	{
		Timer = this;
		//text.enabled = true;
		time = 10;
		rtime = Time.time;
		this.enabled = false;
				text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
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
		if (time<= 0) {
			Manager.manager.charge = false;
			shooter.Shoots();
			progressBar.value = 0;
			time = 10;
			text.text = time.ToString();
		}
	}

	public void ResetTimer(bool enable)
	{
		this.enabled = enable;
		text.enabled = enable;
		time = 10;
		text.text = time.ToString();
	}
}