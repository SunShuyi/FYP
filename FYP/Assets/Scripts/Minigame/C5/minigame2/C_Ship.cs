using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_Ship : MonoBehaviour {

	public Slider ProgressBar1;
	public Slider ProgressBar2;
	public Slider ProgressBar;
	public float sliderValue1;
	public float sliderValue2;
	float sliderValue;
	public TimerCountdown hpbar;
	Vector3 ShipPos;
	Vector3 lastShipPos;
	C_Input theInput = null;
	public float shipSpeed_normal;
	public float shipSpeed_collided;

	private bool isMoving = false;
	
	public GameObject instrucPage;
	private bool instructions;
	private bool playGame;
	
	//	public bool isDead;
	public float damagecd = 0;
	public Animator shipAnim;
	Rigidbody2D shipRB;
	
	
	private float tiltAngles;
	
	public Text theStrength;
	public Text theStrengthShadow;
	public Text  size;

	public StrengthIndicator  StrengthScript;
	public StrengthIndicator StrengthScriptShadow;


	public int StrengthCount;

	public int collidedShips = 0;
	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		lastShipPos= transform.position;
		
		shipAnim = GetComponent<Animator> ();
		shipRB = GetComponent<Rigidbody2D> ();
		
		instructions = true;
		playGame = false;
		
		tiltAngles = Input.acceleration.x;
		
		
		StrengthScript = theStrength.GetComponent<StrengthIndicator> ();
		StrengthScriptShadow = theStrengthShadow.GetComponent<StrengthIndicator> ();

	}
	
	void Update () {
		if (damagecd > 0)
			damagecd -= Time.deltaTime;
		theInput.InputUpdate ();
		
		StrengthCount = StrengthScript.StrengthCount;
		
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		
		if (Input.GetKeyDown ("space") && instructions) //Start the game 
		{ 
			instructions = false; Time.timeScale = 1;
		
			hpbar.enabled = true;
		
		}
		
		
		if (Input.GetKeyDown ("r")) 
		{ Reset (); }
		
		//Click/Tap on left or right side to move the boat
		if(playGame) {
			if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown("d"))//right 
			{ if (this.transform.rotation.z > -0.2f){ this.transform.Rotate(Vector3.forward * -0.2f);
				print (this.transform.rotation);
					transform.Translate (Vector3.right * Time.deltaTime * shipSpeed_normal)	;
				}
			} 
			
			if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown("a")) //left 
			{ if (this.transform.rotation.z < 0.2f){ this.transform.Rotate(Vector3.back* -0.2f); 
					transform.Translate (Vector3.left * Time.deltaTime * shipSpeed_normal)	;
				}
			}
		}
		
		#else
		if(theInput.I_Down && instructions)
		{ instructions = false; }
		
		tiltAngles = Input.acceleration.x;
		if(playGame){
			if(tiltAngles> 0.2) 

			{ this.transform.Rotate(Vector3.forward * -0.5f); }
			
			if(tiltAngles < -0.2) 
			{ this.transform.Rotate(Vector3.back* -0.5f); }
		}
		
		
		#endif
		

		
		if (ShipPos.x > 5) 
		{ transform.Translate (Vector3.left * Time.deltaTime * 5.0f); }
		
		else if (ShipPos.x < -5)
		{ transform.Translate (Vector3.right * Time.deltaTime * 5.0f); }
		
		//Boat moves slowly over time 
		if (playGame && !instructions)
		{
			if(shipRB.velocity != new Vector2(0,0)) 
			transform.Translate (Vector3.up * Time.deltaTime * shipSpeed_collided);
			else
			{
			 	transform.Translate (Vector3.up * Time.deltaTime * shipSpeed_normal); 

			}
		}

		else if (!instructions && !playGame) {
			DestroyObject(instrucPage);
			playGame = true;
		}
		
		//Progress Bar
		ShipPos = new Vector3 (transform.position.x, transform.position.y, 10);	
		if(sliderValue2 < 75)
			sliderValue1 += Time.deltaTime * 12;
		if (sliderValue1 >= 75) {
			sliderValue1 = 0;
			sliderValue2 += 25;
			ProgressBar2.value = sliderValue2;
		} else 
		ProgressBar1.value = sliderValue1;
		
		//GameObject closestRock = FindClosestRock ();
		
		//DetectRocks ();
		
		//if (DetectRocks () && playGame) 

		//{ moveTo(closestRock.transform.position); }
		
		if (ShipPos != lastShipPos) {
			isMoving = true;
		} else {
			isMoving = false;
		}
		
		AnimateShip ();
		
		lastShipPos = ShipPos;
		
	}
	
	void moveTo (Vector3 targetPos)
	{
		if (ShipPos.x < targetPos.x) { //left
			this.transform.Rotate(Vector3.forward * -0.01f);
			
		} else {
			
		}
		
		if (ShipPos.x > targetPos.x) { //right
			this.transform.Rotate(Vector3.back * -0.01f);
			
		} else {
			
			
		}
		
	}
	
	void AnimateShip()
	{
		if (isMoving) 
			shipAnim.SetBool ("isMoving", true);
		else
			shipAnim.SetBool ("isMoving", false);
	}
	
	private bool DetectRocks (){
		if (Physics2D.OverlapCircle(ShipPos, 3.0f))
		{ return true; }
		else
			return false;
	}
	
	GameObject FindClosestRock()
	{
		GameObject[] rocks;
		rocks = GameObject.FindGameObjectsWithTag ("Rocks");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach (GameObject rock in rocks) {
			Vector3 diff = rock.transform.position - position;
			float currDistance = diff.sqrMagnitude;
			if(currDistance < distance)
			{
				closest = rock;
				distance = currDistance;
			}
		}
		return closest;
	}
	
//	//Collision for Exit
//	void OnTriggerEnter2D(Collider2D coll)
//	{
//		if (coll.CompareTag ("Exit")) {
//			Application.LoadLevel ("C4_ShipDeck");
//		} else if (coll.CompareTag ("Rocks")) {
//			if (StrengthCount == 0) {
//				shipAnim.SetBool ("isDead", true);
//				shipRB.velocity = Vector2.zero;
//			} else {
//				StrengthScript.SendMessage ("Hit");
//				StrengthScriptShadow.SendMessage ("Hit");
//				shipRB.AddForce (Vector3.down * 0.04f);
//				shipRB.drag = 2.0f;
//			}
//		} 
//	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.CompareTag ("enemy")) {
			collidedShips++;

			if(collidedShips >= 2)
			{
				shipAnim.SetBool ("isDead", true);
				shipRB.velocity = Vector2.zero;

			}
		}
	}
	
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.collider.CompareTag ("enemy")) {
			collidedShips--;
		}
	}
	void Reset ()
	{ Application.LoadLevel ("MiniGame2"); }
	
}