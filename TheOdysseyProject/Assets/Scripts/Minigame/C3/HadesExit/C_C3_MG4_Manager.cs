using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_C3_MG4_Manager : MonoBehaviour
{
	[Header("GameObjects and References")]
	public GameObject instructionsImg	= null;
	public Slider progressBar			= null;
	public GameObject ghosts			= null;
	public Animator caveAnimator		= null;
	public Animator stairsAnimator		= null;

	[Header("Values")]
	public float speed					= 1.0f;
	public string nextScene				= "";

	[HideInInspector]
	public bool showInstructions		= true;

	// List of ghosts
	private C_C3_MG4_Ghost[] _ghosts	= null;
	
	void Awake ()
	{
		if (ghosts != null)
			_ghosts = ghosts.GetComponentsInChildren<C_C3_MG4_Ghost> (true);

		// to stop animations from playing
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		C_Input.getInstance.InputUpdate ();
		
		if (showInstructions) {
			if (C_Input.getInstance.I_Up || Input.GetKeyUp (KeyCode.Space)) {
				Time.timeScale = 1;
				Destroy (instructionsImg);
				showInstructions = false;
			}
			return;
		}

		bool gotHit = false;
		foreach(C_C3_MG4_Ghost ghost in _ghosts)
		{
			if(ghost.appeared)
			{
				if(C_Input.getInstance.I_Up)
				{
					Vector2 clickedSpot = (Vector2)Camera.main.ScreenToWorldPoint(C_Input.getInstance.I_Up_Position);
					if(ghost.collider.OverlapPoint(clickedSpot))
					{
						ghost.ResetGhost();
					}
				}

				gotHit = true;
				break;
			}
		}

		if (!gotHit) {
			caveAnimator.speed = 1;
			stairsAnimator.speed = 1;
			progressBar.value += Time.deltaTime * speed;
		} else {
			caveAnimator.speed = -1;
			stairsAnimator.speed = -1;
			progressBar.value -= Time.deltaTime * speed * 2;
		}

		if (progressBar.value == progressBar.maxValue)
			Application.LoadLevel (nextScene);

	}

}
