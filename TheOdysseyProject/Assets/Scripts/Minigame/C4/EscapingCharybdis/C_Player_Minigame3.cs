using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_Player_Minigame3 : MonoBehaviour {

	C_Input theInput = null;

	public GameObject theWhirlpool;
	CircleCollider2D theWhirlpoolCol;

	Animator whirlAnim;
	Animator playerAnim;

	public float playerForce;
	
	public bool playGame;
	private bool instructions;

	public GameObject instrucPage;

	GameObject[] NumofDebris;

	Rigidbody2D rb;

	private Vector2 touchPosition;

	public Slider StaminaBar;
	float StaminaPts;
	public float StamDrain;
	
	// Use this for initialization
	void Start () {
		theInput = C_Input.getInstance;
		instructions = true;
		playGame = false;
		
		theWhirlpoolCol = theWhirlpool.GetComponent<CircleCollider2D> ();

		whirlAnim = theWhirlpool.GetComponent<Animator> ();
		playerAnim = GetComponent<Animator> ();

		NumofDebris = GameObject.FindGameObjectsWithTag ("Debris");

		rb = this.gameObject.GetComponent<Rigidbody2D> ();

		if (Input.touchCount > 0) 
		{ touchPosition = Input.GetTouch (0).position; }

		StaminaPts = StaminaBar.maxValue;
	}
	
	// Update is called once per frame
	void Update () {

		theInput.InputUpdate ();

		transform.rotation = Quaternion.LookRotation (Camera.main.transform.forward);

		if (StaminaPts < StaminaBar.maxValue) 
		{ StaminaPts +=  20f * Time.deltaTime; }

		StaminaBar.value = StaminaPts;

		if (!instructions) {
			DestroyObject(instrucPage);
			playGame = true;
		}

		if (playGame) { //enable the obstacle components
			theWhirlpoolCol.enabled = true;
			//theWhirlpool.transform.Rotate (Vector3.forward * Time.deltaTime * -30);

			for(int i=0; i<NumofDebris.Length; i++)
			{
				NumofDebris[i].GetComponent<C_Debris> ().enabled = true;
			}

			whirlAnim.enabled = true;
			playerAnim.enabled = true;
		}

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		if (Input.GetKey (KeyCode.Space) && instructions) 
		{ instructions = false;}

		if(playGame){
			if(Input.GetKeyDown(KeyCode.Space) && StaminaPts > StamDrain)
			{ rb.AddForce(new Vector2 (1,1) * playerForce); StaminaPts -= StamDrain;}
		}

#else
		if (theInput.I_Down && instructions) 
		{ instructions = false; }

		if(playGame){
			if(theInput.I_Down && StaminaPts > StamDrain)
			{ rb.AddForce(new Vector2 (1,1) * playerForce); StaminaPts -= StamDrain;}

		}

#endif
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.CompareTag ("Exit")) 
		{ Application.LoadLevel("C4_Cutscenes_Ending"); }

		if (coll.CompareTag ("Respawn")) 
		{ Application.LoadLevel("EscapingCharybdis"); }
	}
}
