using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Appear : MonoBehaviour {
	public GameObject Win;

	// Use this for initialization
	void Start () {
		Win.SetActive (false);
	
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
				//Win.enabled = true;
				Win.SetActive(true);
				Win.GetComponent<WinPage>().time =4;
				//Destroy (this.gameObject);
				Destroy(coll.gameObject);
				
			}
			
		}
		
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log("hit");
	}
}
