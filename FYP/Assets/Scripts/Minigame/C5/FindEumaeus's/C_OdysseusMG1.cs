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


	private bool instructions;
	private bool playGame;


	Rigidbody2D playerRB;


	private float tiltAngle;

	public Text theLives;

	public int lifeCount;

	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}

	// Use this for initialization
	void Start () {
		lastPlayerPos = transform.position;//PlayerPos


		playerRB = GetComponent<Rigidbody2D> ();//playerRB

		instructions = true;
		playGame = false;

		tiltAngle = Input.acceleration.x;

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
		
			playGame = true;
		}
	
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
	

		if (PlayerPos != lastPlayerPos) {//PlayerPos
			isMoving = true;
		} else {
			isMoving = false;
		}


		lastPlayerPos = PlayerPos;//PlayerPos
		
	}
	
	void moveTo (Vector3 targetPos)
	{
		if (PlayerPos.x < targetPos.x) { //left//PlayerPos
			this.transform.Rotate(Vector3.forward * -0.2f);
		
		}

		if (PlayerPos.x > targetPos.x) { //right //PlayerPos
			this.transform.Rotate(Vector3.back * -0.2f);

		} 

	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) {
			Application.LoadLevel ("FindEumaeus's");
		}
	
		Vector3 heading = coll.transform.position - this.transform.position;
		float Distance = heading.magnitude;
		Vector3 Direction = heading / Distance;
		playerRB.AddForce (-Direction * 0.01f * bounceVal);
		playerRB.drag = 2.0f;
		//}
		if (coll.CompareTag ("wall")) {
			ProgressBar.value -= 15;
		}

		if (coll.CompareTag ("enemy")) {
			ProgressBar.value -= 15;
		}

			if (coll.CompareTag ("bonus")) { 
				Destroy(coll.gameObject);
				ProgressBar.value += 15;
		
		}
	}
	//}

	void Reset ()
	{ Application.LoadLevel ("FindEumaeus's"); }

}