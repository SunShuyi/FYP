using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_OdysseusMG1 : MonoBehaviour {

	public Slider ProgressBar;
	float sliderValue;
	Vector3 PlayerPos;//PlayerPos
	Vector3 lastPlayerPos;//PlayerPos
	C_Input theInput = null;
	public float playerSpeed_normal;//playerSpeed
	public float playerSpeed_lured;//playerSpeed
	public float bounceVal = 0.1f;
	private bool isMoving = false;

	public GameObject instrucPage;
	private bool instructions;
	private bool playGame;

//	public bool isDead;

	//Animator boatAnim;
	Rigidbody2D playerRB;

	//public GameObject SingLeft;
	//public GameObject SingRight;

	//SpriteRenderer singLeft;
	//SpriteRenderer singRight;

	//AudioSource sirenLeftsong;
	//AudioSource sirenRightsong;

	private float tiltAngle;

	public Text theLives;
	//public Text theLivesShadow;
	//C_Lives lifeScript;
	//C_Lives lifeScriptShadow;
	public int lifeCount;

	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}

	// Use this for initialization
	void Start () {
		lastPlayerPos = transform.position;//PlayerPos

		//boatAnim = GetComponent<Animator> ();
		playerRB = GetComponent<Rigidbody2D> ();//playerRB

		instructions = true;
		playGame = false;

		tiltAngle = Input.acceleration.x;

		//singLeft = SingLeft.GetComponent<SpriteRenderer> ();
		//singRight = SingRight.GetComponent<SpriteRenderer> ();
	//	lifeScript = theLives.GetComponent<C_Lives> ();
		//lifeScriptShadow = theLivesShadow.GetComponent<C_Lives> ();
	}

	void Update () {

		theInput.InputUpdate ();

		//lifeCount = lifeScript.lifeCount;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN

		if (Input.GetKeyDown ("space") && instructions) { //Start the game
			instructions = false;
		}


		if (Input.GetKeyDown ("r")) {
			Reset ();
		}

		//Click/Tap on left or right side to move the boat
		if (playGame) {
			//if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown("d"))//right 
			//{ this.transform.Rotate(Vector3.forward * -0.5f); } 
				
			//if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown("a")) //left 
			//{ this.transform.Rotate(Vector3.back* -0.5f); }
		}

#else
		if(theInput.I_Down && instructions)
		{ instructions = false; }

		tiltAngle = Input.acceleration.x;
		if(playGame){
				if(tiltAngle > 0.2) 
			    { this.transform.Rotate(Vector3.forward * -0.5f); }
				
				if(tiltAngle < -0.2) 
			    { this.transform.Rotate(Vector3.back* -0.5f); }
		}

	
#endif
		
		if (!instructions) {
			DestroyObject (instrucPage);
			playGame = true;
		}
	
		//if (BoatPos.x > 5) {
		//	transform.Translate (Vector3.left * Time.deltaTime * 5.0f);
		//}

		//if (BoatPos.x < -5) {
		//	transform.Translate (Vector3.right * Time.deltaTime * 5.0f);
		//}

		//Boat moves slowly over time 
		if (playGame && !instructions) { 
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKeyDown ("w"))
				transform.Translate (Vector3.up * Time.deltaTime * playerSpeed_normal * 5);
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKeyDown ("a"))
				transform.Translate (Vector3.left * Time.deltaTime * playerSpeed_normal * 5); 
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKeyDown ("s"))
				transform.Translate (Vector3.down * Time.deltaTime * playerSpeed_normal * 5); 
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKeyDown ("d"))
				transform.Translate (Vector3.right * Time.deltaTime * playerSpeed_normal * 5); 
	
		}
		//Progress Bar
		PlayerPos = new Vector3 (transform.position.x, transform.position.y, 10);	//PlayerPos
		//sliderValue = BoatPos.y;
		//ProgressBar.value = sliderValue;
		
		//GameObject closestRock = FindClosestRock ();

		//DetectRocks ();

		//if (DetectRocks () && playGame) 
		//{ moveTo(closestRock.transform.position); }

		if (PlayerPos != lastPlayerPos) {//PlayerPos
			isMoving = true;
		} else {
			isMoving = false;
		}

		//AnimateBoat ();

		lastPlayerPos = PlayerPos;//PlayerPos
		
	}
	
	void moveTo (Vector3 targetPos)
	{
		if (PlayerPos.x < targetPos.x) { //left//PlayerPos
			this.transform.Rotate(Vector3.forward * -0.2f);
		//	singRight.enabled = true;
			//SingRight.SetActive(true);
		}

		if (PlayerPos.x > targetPos.x) { //right //PlayerPos
			this.transform.Rotate(Vector3.back * -0.2f);
			//	singLeft.enabled = true;
			   // SingLeft.SetActive(true);
		} 

	}

	//void AnimateBoat()
	//{
		//if (isMoving) 
			//boatAnim.SetBool ("isMoving", true);
		//else
			//boatAnim.SetBool ("isMoving", false);
	//}

	//private bool DetectRocks (){
		//if (Physics2D.OverlapCircle(BoatPos, 3.0f))
		//{ return true; }
		//else
		//return false;
   // }

	//GameObject FindClosestRock()
	//{
		//GameObject[] rocks;
		//rocks = GameObject.FindGameObjectsWithTag ("Rocks");
		//GameObject closest = null;
		//float distance = Mathf.Infinity;
		//Vector3 position = transform.position;

		//foreach (GameObject rock in rocks) {
			//Vector3 diff = rock.transform.position - position;
			//float currDistance = diff.sqrMagnitude;
			//if(currDistance < distance)
			//{
				//closest = rock;
				//distance = currDistance;
			//}
		//}
		//return closest;
	//}

	//Collision for Exit
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) 
		{ Application.LoadLevel("FindEumaeus's"); }

		//if (coll.CompareTag ("Rocks")) {
			//if(lifeCount == 0) {
				//boatAnim.SetBool("isDead", true);
			//	boatRB.velocity = Vector2.zero;
			//} else {
		//lifeScript.SendMessage("Hit");
		//lifeScriptShadow.SendMessage("Hit");
		Vector3 heading = coll.transform.position - this.transform.position;
		float Distance = heading.magnitude;
		Vector3 Direction = heading / Distance;
		playerRB.AddForce(-Direction * 0.01f * bounceVal);
		playerRB.drag = 2.0f;
			//}
		if (coll.CompareTag ("wall")) {
			ProgressBar.value-=15;
		}
	}
	//}

	void Reset ()
	{ Application.LoadLevel ("FindEumaeus's"); }

}