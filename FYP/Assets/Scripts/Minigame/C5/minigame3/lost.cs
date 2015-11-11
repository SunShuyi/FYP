using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lost : MonoBehaviour {

	public  int time1;
	private float rtime1;
	//public Image Lost;
	private int seconds = 0;
	
	// Use this for initialization
	void Start () {
		//text.enabled = true;
		//time1 = 4;
		rtime1 = Time.time;
		seconds = time1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - rtime1 > 1.0f) 
		{
			seconds -=1;
			rtime1= Time.time;
			//seconds =(time1*1);
			//text.text = seconds.ToString();
			if(seconds <= 0)
			{
				Color color = GetComponent<SpriteRenderer>().color;
				color.a = 1;
				GetComponent<SpriteRenderer>().color = color;
				seconds = time1;
				this.gameObject.SetActive(false);
				//this.enabled = false;
				//text.enabled = false;
			}
			
			
		}
		if (time1<= 0) {
			//Lost.enabled = false;
			
		}
	}
	
}
