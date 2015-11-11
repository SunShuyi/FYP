using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public Vector3 target = new Vector3 (0, 0, 0);
	public float Vel = 1;
	public float speed = 5;
	public int lifeWhenShot = 0;
	public float friction = 0.1f;
	public float hitChance = 100;
	bool hit = false; 
	bool Win = false; 
	private bool moving = false;
	public Animator arrowAnim;
	Rigidbody2D arrowRB;

	public GameObject Lose;
	// Use this for initialization
	void Start () {
		//Lose = GameObject.Find ("lost");
		//Lose.SetActive (false);
		arrowAnim = GetComponent<Animator> ();
		arrowRB = GetComponent<Rigidbody2D> ();
		int randVal = (int)Random.Range (1, 100);
		if (hitChance - randVal > 0) {
			hit = true;

		} else
			Destroy (GetComponent<Collider2D> ());
		Debug.Log("randVal: " + randVal + ", hitChance: " + hitChance);
	}
	
	// Update is called once per frame
	void Update () {

		AnimateArrow ();
		transform.position = Vector3.MoveTowards (transform.position,target, Vel * Time.deltaTime * speed);
		//if(Vel > 0) 
		//Vel -= friction;
		if (transform.position == target || Vel <= 0) {
			Destroy(this.gameObject);
			if(!Win)
			{
				Lose.SetActive(true);
			}
			if (lifeWhenShot == 0) {
				Application.LoadLevel ("MiniGame3");


			}
		}
	}

	void AnimateArrow()
	{
		if (moving) 
			arrowAnim.SetBool ("moving", true);
		else
			arrowAnim.SetBool ("moving", false);
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "axe") 
		{
			Win = true;
		}
		
	}
}
