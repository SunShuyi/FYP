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
	public CountDownTimer hpbar;
	private bool instructions;
	private bool playGame;

	public bool GodMode;

	Rigidbody2D playerRB;


	private float tiltAngle;

	public float moveSpeed;
	public AnimationClip Up;
	public AnimationClip Down;
	public AnimationClip Left;
	public AnimationClip Right;
	public AnimationClip Idle;


	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}

	// Use this for initialization
	void Start () {

		moveSpeed = 4 * Time.deltaTime;//TEST
		lastPlayerPos = transform.position;//PlayerPos


		playerRB = GetComponent<Rigidbody2D> ();//playerRB

		instructions = true;
		playGame = false;

		tiltAngle = Input.acceleration.x;

	}

	void Update () 
	{

		theInput.InputUpdate ();

	

#if UNITY_EDITOR || UNITY_STANDALONE_WIN

		if (Input.GetKeyDown ("space") && instructions) { //Start the game
			instructions = false;
			hpbar.enabled = true;
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

			DestroyObject(instrucPage);
			playGame = true;
		}
	
		if (playGame && !instructions )  {

			if (!instructions) 
			{
				
				DestroyObject(instrucPage);
				playGame = true;
			}

			if (!InstructionsManager.b_IsDone)
				GetComponent<Animator> ().SetInteger ("Type", 0);
			
			if (playGame && !instructions && InstructionsManager.b_IsDone) 
			{
				
				
				Vector2 temp = transform.position;
				
				switch ((int)this.transform.eulerAngles.z) 
				{
					
				case 0:
					if (Input.GetKey (KeyCode.UpArrow))
					{
						temp.y += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 4);
					} 
					else if (Input.GetKey (KeyCode.DownArrow))
					{
						temp.y -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 3);
					}
					else if (Input.GetKey (KeyCode.LeftArrow))
					{
						temp.x -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 1);
					} 
					else if (Input.GetKey (KeyCode.RightArrow))
					{
						temp.x += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 2);
					}
					else
						GetComponent<Animator> ().SetInteger ("Type", 0);
					break;
				case 90:
					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						temp.x -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 1);
					} 
					else if (Input.GetKey (KeyCode.DownArrow))
					{
						
						temp.x += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 2);
					} 
					else if (Input.GetKey (KeyCode.LeftArrow)) 
					{
						
						temp.y -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 4);
					}
					else if (Input.GetKey (KeyCode.RightArrow))
					{
						
						temp.y += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 3);
						
					} 
					else
						GetComponent<Animator> ().SetInteger ("Type", 0);
					break;
				case 180:
					if (Input.GetKey (KeyCode.UpArrow))
					{
						
						temp.y -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 3);
					} 
					else if (Input.GetKey (KeyCode.DownArrow)) 
					{
						
						temp.y += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 4);
					} 
					else if (Input.GetKey (KeyCode.LeftArrow))
					{
						
						temp.x += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 1);
					} 
					else if (Input.GetKey (KeyCode.RightArrow)) 
					{
						
						temp.x -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 2);
						
					} else
						GetComponent<Animator> ().SetInteger ("Type", 0);
					break;
				case 270:
					if (Input.GetKey (KeyCode.UpArrow))
					{
						
						temp.x += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 2);
					} 
					else if (Input.GetKey (KeyCode.DownArrow)) 
					{
						
						temp.x -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 1);
						
					} 
					else if (Input.GetKey (KeyCode.LeftArrow)) 
					{
						
						temp.y += moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 4);
						
					} 
					else if (Input.GetKey (KeyCode.RightArrow))
					{
						
						temp.y -= moveSpeed;
						transform.position = temp;
						GetComponent<Animator> ().SetInteger ("Type", 3);
						
					} else
						GetComponent<Animator> ().SetInteger ("Type", 0);
					break;
					
				}
			}
			//Progress Bar
			PlayerPos = new Vector3 (transform.position.x, transform.position.y, 10);	//PlayerPos
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


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) {
			Application.LoadLevel ("C5_EumaeusHutLivingRoom");
		}
	

//
//		Vector3 heading = coll.transform.position - this.transform.position;
//		float Distance = heading.magnitude;
//		Vector3 Direction = heading / Distance;
//		playerRB.AddForce (-Direction * 0.01f * bounceVal);
//		playerRB.drag = 2.0f;
		//}
		if (!GodMode) 
		{
			if (coll.CompareTag ("wall")) 
			{
				//ProgressBar.value -=3;			
				instructions = true;
			}

			if (coll.CompareTag ("enemy")) 
			{
////				Vector3 heading = coll.transform.position - this.transform.position;
////						float Distance = heading.magnitude;
////						Vector3 Direction = heading / Distance;
////						playerRB.AddForce (-Direction * 0.01f * bounceVal);
////						playerRB.drag = 2.0f;
//
				ProgressBar.value -=3;
//				instructions = true;
//				playerRB.AddForce (-Direction * bounceVal * 0.05f);
//				coll.gameObject.GetComponent<wayPoint>().Combat = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		//if (!coll.CompareTag ("enemy")) {
//			Vector3 heading = coll.transform.position - this.transform.position;
//			float Distance = heading.magnitude;
//			Vector3 Direction = heading / Distance;
//			playerRB.AddForce (-Direction * 0.01f * bounceVal);
//			playerRB.drag = 2.0f;
		//}

		if (!GodMode) 
		{
			if (coll.CompareTag ("enemy")) 
			{
				InstructionsManager.Instance.CreateInstruction(E_Instruct_Type.INSTRUCT_WOLF);
				////				Vector3 heading = coll.transform.position - this.transform.position;
				////						float Distance = heading.magnitude;
				////						Vector3 Direction = heading / Distance;
				////						playerRB.AddForce (-Direction * 0.01f * bounceVal);
				////						playerRB.drag = 2.0f;
				//

				if (InstructionsManager.b_IsDone)
					ProgressBar.value -= 10*Time.deltaTime;
				//				instructions = true;
				//				playerRB.AddForce (-Direction * bounceVal * 0.05f);
				//				coll.gameObject.GetComponent<wayPoint>().Combat = true;
			}

			if (coll.CompareTag ("bonus")) 
			{ 
				InstructionsManager.Instance.CreateInstruction(E_Instruct_Type.INSTRUCT_MEAT);

				if (InstructionsManager.b_IsDone)
				{
					Destroy(coll.gameObject);
					ProgressBar.value += 5;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		instructions = false;

	}

	void Reset ()
	{ 
		Application.LoadLevel ("C5_Phaeacians'Forest1"); }

}