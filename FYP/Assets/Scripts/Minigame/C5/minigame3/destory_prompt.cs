using UnityEngine;
using System.Collections;

public class destory_prompt : MonoBehaviour {

	public  int time1;
	private float rtime1;
	//public Image Lost;
	private int seconds = 6;
	
	// Use this for initialization
	void Start () {
		//text.enabled = true;
		time1 = 6;
		rtime1 = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - rtime1 > 1.0f) 
		{
			time1 -=1;
			rtime1= Time.time;
			seconds =(time1*1);
			//text.text = seconds.ToString();
			if(seconds <= 0)
			{
				this.enabled = false;
				//text.enabled = false;
			}
			
			
		}
		if (time1<= 0) {
			//Lost.enabled = false;
			Destroy (this.gameObject);
			
		}
	}
	
}