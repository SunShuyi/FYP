using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_Boat : MonoBehaviour {

	public Slider ProgressBar;
	float sliderValue;
	Vector3 BoatPos;
	Vector3 lastBoatPos;
	C_Input theInput = null;
	public float boatSpeed_normal;
	public float boatSpeed_lured;

	private bool isMoving = false;

	public GameObject instrucPage;
	private bool instructions;
	private bool playGame;

//	public bool isDead;

	Animator boatAnim;
	Rigidbody2D boatRB;

	public GameObject SingLeft;
	public GameObject SingRight;

	SpriteRenderer singLeft;
	SpriteRenderer singRight;

	AudioSource sirenLeftsong;
	AudioSource sirenRightsong;

	private float tiltAngle;

	public Text theLives;
	public Text theLivesShadow;
	C_Lives lifeScript;
	C_Lives lifeScriptShadow;
	public int lifeCount;

	void Awake()
	{
		if(theInput == null)
			theInput = C_Input.getInstance;
	}

	// Use this for initialization
	void Start () {
		lastBoatPos = transform.position;

		boatAnim = GetComponent<Animator> ();
		boatRB = GetComponent<Rigidbody2D> ();

		instructions = true;
		playGame = false;

		tiltAngle = Input.acceleration.x;

		singLeft = SingLeft.GetComponent<SpriteRenderer> ();
		singRight = SingRight.GetComponent<SpriteRenderer> ();
		lifeScript = theLives.GetComponent<C_Lives> ();
		lifeScriptShadow = theLivesShadow.GetComponent<C_Lives> ();
	}

	void Update () {

		theInput.InputUpdate ();

		lifeCount = lifeScript.lifeCount;

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
	
		if (BoatPos.x > 5) 
		{ transform.Translate (Vector3.left * Time.deltaTime * 5.0f); }

		if (BoatPos.x < -5)
		{ transform.Translate (Vector3.right * Time.deltaTime * 5.0f); }

		//Boat moves slowly over time 
		if (playGame && !instructions) 
		{ transform.Translate (Vector3.up * Time.deltaTime * boatSpeed_normal); }

		//Progress Bar
		BoatPos = new Vector3 (transform.position.x, transform.position.y, 10);	
		sliderValue = BoatPos.y;
		ProgressBar.value = sliderValue;
		
		GameObject closestRock = FindClosestRock ();

		DetectRocks ();

		if (DetectRocks () && playGame) 
		{ moveTo(closestRock.transform.position); }

		if (BoatPos != lastBoatPos) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		AnimateBoat ();

		lastBoatPos = BoatPos;
		
	}
	
	void moveTo (Vector3 targetPos)
	{
			if (BoatPos.x < targetPos.x) { //left
			this.transform.Rotate(Vector3.forward * -0.2f);
		//	singRight.enabled = true;
			SingRight.SetActive(true);
		} else {
		//	singRight.enabled = false;
			SingRight.SetActive(false);

		}

			if (BoatPos.x > targetPos.x) { //right
			this.transform.Rotate(Vector3.back * -0.2f);
			//	singLeft.enabled = true;
			    SingLeft.SetActive(true);
		} else {
			//	singLeft.enabled = false;
			    SingLeft.SetActive(false);
			
		}

	}

	void AnimateBoat()
	{
		if (isMoving) 
			boatAnim.SetBool ("isMoving", true);
		else
			boatAnim.SetBool ("isMoving", false);
	}

	private bool DetectRocks (){
		if (Physics2D.OverlapCircle(BoatPos, 3.0f))
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
		if (coll.CompareTag ("Exit")) 
		{ Application.LoadLevel("C4_ShipDeck"); }

		if (coll.CompareTag ("Rocks")) {
			if(lifeCount == 0) {
				boatAnim.SetBool("isDead", true);
				boatRB.velocity = Vector2.zero;
			} else {
				lifeScript.SendMessage("Hit");
				lifeScriptShadow.SendMessage("Hit");
				boatRB.AddForce(Vector3.down * 0.04f);
				boatRB.drag = 2.0f;
			}
		}
	}

	void Reset ()
	{ Application.LoadLevel ("PassingtheSirens"); }

}