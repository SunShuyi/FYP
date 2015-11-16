using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

		[Header("GameObjects and References")]
		public GameObject instructionsImg	= null;
		public Slider progressBar			= null;
		[Header("Values")]
		public float speed	= 1.0f;
		//public string nextScene				= "";
		public 	float sliderValue =0;
		
		[HideInInspector]
		public bool showInstructions		= true;

		public bool charge = false;
		float cDir = 1;

		public static Manager manager;

       	//public Animator shootAnim;
	     // public bool ready = false;
		//public C_Lives lifeScript =    null ;
		//public C_Lives lifeScriptShadow = null;
//		public int lifeCount = 0;
//		
//		public Text theLives = null ;
//		public Text theLivesShadow= null;
//		
		
		void Start()
		{
			manager = this;
		//shootAnim = GetComponent<Animator> ();
		}
		
		void Awake ()
		{
			Time.timeScale = 0;
			
//			lifeScript = theLives.GetComponent<C_Lives> ();
//			lifeScriptShadow = theLivesShadow.GetComponent<C_Lives> ();
			
		}
		
	void OnMouseDown()
	{
		if (Lives.life.lifeCount > 0) {
			
			charge = true;
			Minigame2Timer.Timer.enabled = true;
			Minigame2Timer.Timer.text.enabled = true;
			//shootAnim.SetBool ("ready", true);
			
		}
	}

		// Update is called once per frame
		void Update ()
		{
			C_Input.getInstance.InputUpdate ();
		//AnimateShoot();
			
//			lifeCount = lifeScript.lifeCount;
			
			if (showInstructions) {
				if (C_Input.getInstance.I_Up || Input.GetKeyUp (KeyCode.Space)) {
					Time.timeScale = 1;
					Destroy (instructionsImg);
					showInstructions = false;
				}
				if (Input.touchCount > 0) {
					
					if(Input.GetTouch(0).phase == TouchPhase.Began)
					{
						Time.timeScale = 1;
						Destroy (instructionsImg);
						showInstructions = false;
					}
				}
				//return;
			}

		if (Input.touchCount > 0) {

			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{

				//if (Lives.life.lifeCount > 0) {
					
					charge = true;
					Minigame2Timer.Timer.enabled = true;
					Minigame2Timer.Timer.text.enabled = true;
					//shootAnim.SetBool ("ready", true);
					
				//}

			}
		}
			
			//bool gotHit = false;
			


		if (Input.GetKeyDown (KeyCode.Backspace) && Lives.life.lifeCount > 0) {

			charge = true;
			Minigame2Timer.Timer.enabled = true;
			Minigame2Timer.Timer.text.enabled = true;
			//shootAnim.SetBool ("ready", true);
	
		}

//		if (Input.GetKeyDown ("r")) 
//		{ Reset (); 
//		
//		}
//			else 
		if(charge)
			{
				progressBar.value += Time.deltaTime * speed * 100 * cDir;
				if(progressBar.value <= progressBar.minValue || progressBar.value >= progressBar.maxValue)
					cDir = -cDir;
			}
			//if (!gotHit) {
			//	
			//	progressBar.value += Time.deltaTime * speed*20;
			//} else {
				
				//progressBar.value -= Time.deltaTime * speed * 2;
				//progressBar.value = sliderValue;
			    //progressBar.value -= Time.deltaTime * speed*20;
			//}
			
			//if (progressBar.value == progressBar.maxValue) 
			//Application.LoadLevel (nextScene);
			
			
		}
//
//	void AnimateShoot()
//	{
//		if (ready) 
//			shootAnim.SetBool ("ready", true);
//		else
//			shootAnim.SetBool ("ready", false);
//	}
	void Reset ()
	{ Application.LoadLevel ("MiniGame3"); }
		
	}
