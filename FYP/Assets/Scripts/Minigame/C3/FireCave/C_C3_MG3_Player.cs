using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_C3_MG3_Player : MonoBehaviour
{
	#region Variables

	[Header("Prefabs")]
	// Explosion when tile break
	public GameObject explosionPrefab			= null;
	// Sprite animation over player when on fire
	public GameObject firePrefab				= null;
	
	[Header("Sprites - Tiles")]
	public Sprite tileSprite_Breakable			= null;
	public Sprite tileSprite_Breaking			= null;
	
	[Header("GameObjects and References")]
	// Instructions screen
	public GameObject instructionsImg			= null;
	public GameObject theEnvironment			= null;
	public Collider2D theEnd					= null;
	public Image healthUI						= null;
	public Image fireDebuffIcon					= null;
	public Image BurnUI                         = null;
	public GameObject BurnParticles             = null;
	
	[Header("Player's settings")]
	public float speed							= 500.0f;
	public float health							= 100.0f;
	public float debuffTimerInSec				= 3.0f;
	public float debuffDamagePerSec				= 3.0f;
	public float debuffSpeedSlow				= 100.0f;
	[HideInInspector]
	public bool onLava							= false;

	[Space(10)]
	public string nextScene						= "";

	// Lazy instance reference for getting prefabs from other classes
	public static C_C3_MG3_Player instance		= null;

	[HideInInspector]
	public bool showInstructions				= true;

	private Rigidbody2D _rigidbody				= null;

	public Color32 BurnUIColor;

	#endregion
	
	// Use this for initialization
	void Start ()
	{
		instance = this;
		//_rigidbody = gameObject.GetComponent<Rigidbody2D>();
		_rigidbody = theEnvironment.GetComponent<Rigidbody2D>();

		BurnUIColor = BurnUI.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		C_Input.getInstance.InputUpdate ();

		if (showInstructions) {
			if (C_Input.getInstance.I_Up || Input.GetKeyUp (KeyCode.Space)) {
				Destroy (instructionsImg);
				showInstructions = false;
			}
			return;
		} else {

			if (fireDebuffIcon.fillAmount > 0.0f) {
				PlayerOnFire ();
				MovePlayer (true);
			} else {
				
				MovePlayer ();
			}
			healthUI.fillAmount = health / 100.0f;
		}

		if (BurnUIColor.a > 0) {
			BurnUIColor.a -= 5;
			BurnParticles.SetActive(true);
		} else {
			BurnParticles.SetActive(false);
		}

		BurnUI.color = BurnUIColor;
	}

	// Actually moves the environment, so controls are flipped
	void MovePlayer(bool onFire = false)
	{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

		if(onFire)
		{
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				_rigidbody.AddForce(Vector2.right*Time.deltaTime*(speed-debuffSpeedSlow));
			else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				_rigidbody.AddForce(-Vector2.right*Time.deltaTime*(speed-debuffSpeedSlow));
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				_rigidbody.AddForce(Vector2.up*Time.deltaTime*(speed-debuffSpeedSlow));
			else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				_rigidbody.AddForce(-Vector2.up*Time.deltaTime*(speed-debuffSpeedSlow));
		}
		else
		{
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				_rigidbody.AddForce(Vector2.right*Time.deltaTime*speed);
			else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				_rigidbody.AddForce(-Vector2.right*Time.deltaTime*speed);
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				_rigidbody.AddForce(Vector2.up*Time.deltaTime*speed);
			else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				_rigidbody.AddForce(-Vector2.up*Time.deltaTime*speed);
		}

#elif UNITY_ANDROID
		
		if(C_Input.getInstance.I_Hold)
		{
			Vector2 tempForce = -(Vector2)Camera.main.ScreenToWorldPoint(C_Input.getInstance.I_Hold_Position).normalized;
			if(onFire)
				_rigidbody.AddForce(tempForce*Time.deltaTime*(speed-debuffSpeedSlow));
			else
				_rigidbody.AddForce(tempForce*Time.deltaTime*speed);
		}

#endif
	}

	void PlayerOnFire()
	{
		if(!onLava)
			fireDebuffIcon.fillAmount -= Time.deltaTime/debuffTimerInSec;
		    health -= Time.deltaTime*debuffDamagePerSec;
		    BurnUIColor.a = 255;
		if (health <= 0.0f)
			Application.LoadLevel (Application.loadedLevel);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other == theEnd)
		{
			Application.LoadLevel(nextScene);
		}
	}

}
