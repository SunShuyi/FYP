using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Appear : MonoBehaviour {
	public Image Win;

	// Use this for initialization
	void Start () {
		Win.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "deadly") 
		{
			if(this.gameObject.name != "axe")
			{
				Win.enabled = true;
				Win.GetComponent<WinPage>().enabled=true;
				Win.GetComponent<WinPage>().time = 5;
				//Destroy (this.gameObject);
				
				
			}
			
		}
		
	}
	
}
