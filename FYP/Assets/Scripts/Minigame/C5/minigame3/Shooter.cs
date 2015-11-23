using UnityEngine;
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
	public static Manager manager;
    public Animator shootAnim;
	private bool ready = false;
	public colide colides;
	//private GameObject audioCollect;
	//private bool playGame;
//	private bool instructions;
//	Vector3 PlayerPos;//PlayerPos
//	Vector3 lastPlayerPos;//PlayerPos
//	public float playerSpeed_normal;//playerSpeed

	// Use this for initialization
	void Start () {
		//lastPlayerPos = transform.position;
		//yield return StartCoroutine (Shoots ());
		//audioCollect = GameObject.Find ("SFX");
		lifeScript = theLives.GetComponent<Lives> ();
		lifeScriptShadow = theLivesShadow.GetComponent<Lives> ();
		shootAnim = GetComponent<Animator> ();
		//instructions = true;
		//playGame = false;

	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if(Input.GetTouch (0).phase == TouchPhase.Began)
			{
				if (Lives.life.lifeCount > 0) 
					shootAnim.SetBool ("ready", true);
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				Shoots ();
				Minigame2Timer.Timer.ResetTimer(false);
				
				shootAnim.SetBool ("ready", false);
			}
		}
		//AnimateShoot();

		if(Input.GetKeyUp(KeyCode.Backspace) && Manager.manager.charge && Lives.life.lifeCount > 0)
		{
			Shoots ();
			Minigame2Timer.Timer.ResetTimer(false);
	
			shootAnim.SetBool ("ready", false);
		}

		if(Input.GetKeyDown(KeyCode.Backspace))
		{

			shootAnim.SetBool ("ready", true);
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

	
	void AnimateShoot()
	{
		if (ready) 
			shootAnim.SetBool ("ready", true);
		else
			shootAnim.SetBool ("ready", false);
	}


	void Colide()

	{
		colides.change ();

	}

	public void Shoots()
	{
		Invoke ("Colide", 1);
		//audioCollect.GetComponent<AudioScript>().playOnceCustom(0);


		GameObject clone = (GameObject)Instantiate(projectile,transform.position+new Vector3(-5.3f,-4.92f,0),Quaternion.identity);
//			clone.transform.localRotation = projectile.transform.localRotation;
//			clone.transform.SetLocalPositionY (transform.position.y - 3.19f);
//			clone.GetComponent<Arrow> ().target = transform.position;
//			//clone.GetComponent<Arrow> ().Vel =  progressBar.value * 1.2f;
//			clone.GetComponent<Arrow> ().hitChance = 100-Mathf.Abs((progressBar.value -50) * 2);
			
			Lives.life.lifeCount--;
//			clone.GetComponent<Arrow> ().lifeWhenShot = Lives.life.lifeCount;
//			clone.GetComponent<Arrow> ().Lose = Lose;
//		   clone.GetComponent<Manager> ().ready = false;
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
