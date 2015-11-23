using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class colide : MonoBehaviour {
	public GameObject colides;
	public bool lost = false;
	public bool win = false;
	public int lifeWhenShot = 3;
	public GameObject Lose;
	public GameObject Win;
	public Slider progressBar;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



	
	}

	public void change()
	{

		if(colides == null)
		{
			lost = true;
			
		}
		
		else if (colides.tag == "axe") {
			int randVal = (int)Random.Range (1, 100);
			if ((100-Mathf.Abs((progressBar.value -50) * 2)) - randVal > 0) {
				win = true;
			}

		}

		progressBar.value = 0;

		if(!win)
		{
			Lose.SetActive(true);
			lifeWhenShot--;
			
		}
		else {
			Win.SetActive (true);
			Win.GetComponent<WinPage> ().time = 2;
		}
		if (lifeWhenShot == 0) {
			Application.LoadLevel ("MiniGame3");
			
			
		}

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "axe") 
		{
			colides = coll.gameObject;

		}
		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "axe") 
		{

			colides = null;
		}
		
	}
}
