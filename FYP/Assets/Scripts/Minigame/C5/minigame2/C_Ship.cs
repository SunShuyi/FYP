using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_Ship : MonoBehaviour {

	public Slider ProgressBar;
	public float sliderValue;
	Vector3 ShipPos;
	Vector3 lastShipPos;
	C_Input theInput = null;
	public float shipSpeed_normal;
	public float shipSpeed_lured;
	
	private bool isMoving = false;
	
	public GameObject instrucPage;
	private bool instructions;
	private bool playGame;
	
	//	public bool isDead;
	
	Animator shipAnim;
	Rigidbody2D shipRB;
	
	
	private float tiltAngles;
	
	public Text theStrength;
	public Text theStrengthShadow;
	public Text  size;
	StrengthIndicator  StrengthScript;
	StrengthIndicator StrengthScriptShadow;
	public int StrengthCount;

	int collidedShips = 0;
	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}
	
	// Use this for initialization
	void Start () {
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
		
		theInput.InputUpdate ();
		
		StrengthCount = StrengthScript.StrengthCount;
		
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		
		if (Input.GetKeyDown ("space") && instructions) //Start the game 
		{ instructions = false;}
		
		
		if (Input.GetKeyDown ("r")) 
		{ Reset (); }
		
		//Click/Tap on left or right side to move the boat
		if(playGame) {
			if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown("d"))//right 
			{ this.transform.Rotate(Vector3.forward * -0.5f); } 
			
			if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown("a")) //left 
			{ this.transform.Rotate(Vector3.back* -0.5f); }
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
		
		if (!instructions) {
			DestroyObject(instrucPage);
			playGame = true;
		}
		
		if (ShipPos.x > 5) 
		{ transform.Translate (Vector3.left * Time.deltaTime * 5.0f); }
		
		if (ShipPos.x < -5)
		{ transform.Translate (Vector3.right * Time.deltaTime * 5.0f); }
		
		//Boat moves slowly over time 
		if (playGame && !instructions) 
		{ transform.Translate (Vector3.up * Time.deltaTime * shipSpeed_normal); }
		
		//Progress Bar
		ShipPos = new Vector3 (transform.position.x, transform.position.y, 10);	
		sliderValue += Time.deltaTime * 2;
		ProgressBar.value = sliderValue;
		
		GameObject closestRock = FindClosestRock ();
		
		DetectRocks ();
		
		if (DetectRocks () && playGame) 
		{ moveTo(closestRock.transform.position); }
		
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
			this.transform.Rotate(Vector3.forward * -0.2f);
			
		} else {
			
		}
		
		if (ShipPos.x > targetPos.x) { //right
			this.transform.Rotate(Vector3.back * -0.2f);
			
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
	
	//Collision for Exit
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) {
			Application.LoadLevel ("C4_ShipDeck");
		} else if (coll.CompareTag ("Rocks")) {
			if (StrengthCount == 0) {
				shipAnim.SetBool ("isDead", true);
				shipRB.velocity = Vector2.zero;
			} else {
				StrengthScript.SendMessage ("Hit");
				StrengthScriptShadow.SendMessage ("Hit");
				shipRB.AddForce (Vector3.down * 0.04f);
				shipRB.drag = 2.0f;
			}
		} 
	}

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
	{ Application.LoadLevel ("PassingtheSirens"); }
	
}