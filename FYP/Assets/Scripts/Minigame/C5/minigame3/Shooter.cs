﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooter : MonoBehaviour {
	public GameObject projectile;
	public float speedFactor;
	public float delay;
	public Transform shooter;
	public Slider progressBar;
	//public Vector2 velocity;

	public Text theLives;
	public Text theLivesShadow;
	public Lives lifeScript;
	public Lives lifeScriptShadow;
	public int lifeCount;
	public GameObject Lose;
	//private bool playGame;
//	private bool instructions;
//	Vector3 PlayerPos;//PlayerPos
//	Vector3 lastPlayerPos;//PlayerPos
//	public float playerSpeed_normal;//playerSpeed

	// Use this for initialization
	void Start () {
		//lastPlayerPos = transform.position;
		//yield return StartCoroutine (Shoots ());
		lifeScript = theLives.GetComponent<Lives> ();
		lifeScriptShadow = theLivesShadow.GetComponent<Lives> ();
		//instructions = true;
		//playGame = false;

	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetKeyUp(KeyCode.Backspace) && Manager.manager.charge && Lives.life.lifeCount > 0)
		{
			Shoots ();
			Minigame2Timer.Timer.ResetTimer(false);
		}

		
		if (Input.GetKeyDown ("r")) 
		{ Reset (); 
			
		}
		
//		if (!instructions) {
//			
//			playGame = true;
//		}
		

			 
			

		lifeCount = lifeScript.lifeCount;
		//PlayerPos = new Vector3 (transform.position.x, transform.position.y, 10);	//PlayerPos
	}



	public void Shoots()
	{

			GameObject clone = (GameObject)Instantiate(projectile,transform.position,Quaternion.identity);
			clone.transform.localRotation = projectile.transform.localRotation;
			clone.transform.SetLocalPositionY (transform.position.y - 10);
			clone.GetComponent<Arrow> ().target = transform.position;
			//clone.GetComponent<Arrow> ().Vel =  progressBar.value * 1.2f;
			clone.GetComponent<Arrow> ().hitChance = 100-Mathf.Abs((progressBar.value -50) * 2);
			progressBar.value = 0;
			Lives.life.lifeCount--;
			clone.GetComponent<Arrow> ().lifeWhenShot = Lives.life.lifeCount;
			clone.GetComponent<Arrow> ().Lose = Lose;
			//clone.GetComponent<Rigidbody>().velocity = shooter.forward*progressBar.value *1.2f;

		//if( Lives.life.lifeCount == 0)
		//{
		//	Reset (); 
		//}
		
		Manager.manager.charge = false;
	}
	void Reset ()
	{ Application.LoadLevel ("MiniGame3"); 
	}


//	void OnTriggerEnter2D(Collider2D coll){
//
//
//		if (coll.CompareTag ("deadly")) {
//			if (lifeCount == 0) {
//			}
//
//		}
	//}
}
