using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class open : MonoBehaviour {
	public Image map;
	//public Text text;
	//public GameObject player;
	
	// Use this for initialization
	void Start () {
		map.enabled = false;
		//text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			if(this.gameObject.name != "mapbook")
			{
				map.enabled = true;
				map.GetComponent<timer>().enabled=true;
				map.GetComponent<timer>().time = 5;
				map.GetComponent<timer>().text.enabled = true;
				Destroy (this.gameObject);
				
				
			}
			
		}
		
	}
	
}
