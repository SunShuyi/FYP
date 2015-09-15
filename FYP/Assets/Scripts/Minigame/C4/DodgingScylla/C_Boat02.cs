using UnityEngine;
using System.Collections;

public class C_Boat02 : MonoBehaviour {

	C_Input theInput = null;
	public float boatSpeed_normal;
	public float boatSpeed_slowed;
	public float boatSpeed_max;
	private bool boat_slowed;
	private bool boat_spedup;

	public GameObject instrucPage;
	private bool instructions;
	private bool playGame;

	private float tiltAngle;

	public bool isDead;

	Animator boatAnim;

	// Use this for initialization
	void Start () {
		theInput = C_Input.getInstance;

		instructions = true;
		playGame = false;
		boat_slowed = false;
		boat_spedup = false;

		isDead = false;

		boatAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		theInput.InputUpdate ();

		if (playGame && boatSpeed_normal < boatSpeed_max && boat_spedup) 
		{ boatSpeed_normal += 2f * Time.deltaTime; }

		if (playGame) 
		{ transform.Translate (Vector3.right * Time.deltaTime * boatSpeed_normal); }

		if (playGame && boatSpeed_normal > boatSpeed_slowed && boat_slowed) 
		{ boatSpeed_normal -= 2f * Time.deltaTime; }

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		
		if (Input.GetKeyDown ("space") && instructions) //Start the game 
		{ instructions = false; boatAnim.SetBool("boat_moving", true); }
		
		//Click Tap on left or right side to move the boat
		if(theInput.I_Hold || Input.GetKey(KeyCode.LeftArrow)){
			boat_slowed = true;
		}else{
			boat_slowed = false;
		}

		if(theInput.I_Up || Input.GetKey(KeyCode.RightArrow)){
			boat_spedup = true;
		}else{
			boat_spedup = false;
		}
#else
		tiltAngle = Input.acceleration.x;

		if(theInput.I_Down && instructions)
		{ instructions = false; boatAnim.SetBool("boat_moving", true); }
		
		if(tiltAngle < 0.2){
			boat_slowed = true;
		}else{
			boat_slowed = false;
		}

		if(tiltAngle > 0.2){
			boat_spedup = true;
		}else{
			boat_spedup = false;
		}
#endif
	
		if (!instructions) {
			DestroyObject(instrucPage);
			playGame = true;
		}

		if (isDead) {
			playGame = false;
			boatAnim.SetBool ("boat_die", true);
		}

		//Debug.Log (BoatPos.y);

	}

	float SetBoatSpeed (float new_speed)
	{
		new_speed = boatSpeed_normal;
		return boatSpeed_normal;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) {
			Application.LoadLevel("C4_Cutscenes_Island");
		}

		if (coll.CompareTag ("Wave")) {
			//coll.GetComponent<C_Wave>().
		}
	}

	void Reset ()
	{
		//playGame = false;
		//play boat sinking animation;
		//WaitforSeconds(timeofAnimation);
		Application.LoadLevel ("DodgingScylla");
	}

}
