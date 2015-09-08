using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_C3_MG2_Manager : MonoBehaviour
{
	public string nextScene					= "";
	public GameObject ghost					= null;
	public Transform playerPivot			= null;
	public Transform sheep					= null;
	public Transform bloodPool				= null;
	public GameObject instructionsImg		= null;
	public Slider progressBar				= null;
	public float bloodScaleSpeed			= 1.0f;
	public float ghostSpawnTimerInSec		= 5.0f;
	public float ghostSpawnTimerDiffuse		= 2.0f;

	[HideInInspector]
	public int ghostsDrinking				= 0;

	private bool _gamePlaying				= false;
	private bool _showInstructions			= true;

	private C_C3_MG2_Player player			= null;

	public static C_C3_MG2_Manager instance = null;

	// Use this for initialization
	void Start ()
	{
		instance = this;

		_gamePlaying = true;

		//StartCoroutine (SpawnGhost());

		player = playerPivot.gameObject.GetComponent<C_C3_MG2_Player>();
	
	}

	void Update()
	{
		if (_showInstructions) {
			C_Input.getInstance.InputUpdate ();
			
			if (C_Input.getInstance.I_Up || Input.GetKeyUp(KeyCode.Space)) {
				Destroy (instructionsImg);
				_showInstructions = false;
				player.canUpdate = true;
				StartCoroutine (SpawnGhost ());
			}
			
			return;
		} else {
			if(ghostsDrinking > 0)
			{
				if(progressBar.value - Time.deltaTime*ghostsDrinking >= progressBar.minValue)
				{
					float theValue = Time.deltaTime*ghostsDrinking;
					progressBar.value -= theValue;
					bloodPool.localScale -= new Vector3(1,1,1)*theValue*bloodScaleSpeed;
				}
			}
			else if(progressBar.value + Time.deltaTime <= progressBar.maxValue)
			{
				float theValue = Time.deltaTime;
				progressBar.value += theValue;
				bloodPool.localScale += new Vector3(1,1,1)*theValue*bloodScaleSpeed;
			}
			else
				Application.LoadLevel(nextScene);
		}
	}

	IEnumerator SpawnGhost()
	{
		while(_gamePlaying)
		{
			float randomTime = Random.Range(ghostSpawnTimerInSec-ghostSpawnTimerDiffuse/2,ghostSpawnTimerInSec+ghostSpawnTimerDiffuse/2);
			yield return new WaitForSeconds(randomTime);

			Vector3 spawnPos = RandomSpawnPosition();
			// Instantiated under root will have transform problems
			GameObject ghostClone = (GameObject)Instantiate(ghost, spawnPos,Quaternion.identity);

			ghostClone.GetComponent<C_C3_MG2_Ghost>().sheep = sheep;

			Vector3 vectorToTarget = sheep.position - spawnPos;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle+90f, Vector3.forward);
			ghostClone.transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);

			ghostClone.transform.SetParent(sheep.parent);
			ghostClone.transform.localScale = new Vector3(1,1,1);
			ghostClone.transform.localPosition = spawnPos;
		}
	}

	Vector3 RandomSpawnPosition()
	{
		int randomDirection = Random.Range (1,5);

		Vector2 randPos = new Vector2 (Random.Range(-640,640),Random.Range(-360,360));

		switch(randomDirection)
		{
		// LEFT
		case 1:
			randPos.x = -700;
			break;
		// RIGHT
		case 2:
			randPos.x = 700;
			break;
		// TOP
		case 3:
			randPos.y = 420;
			break;
		// BOTTOM
		case 4:
			randPos.y = -420;
			break;
		default:
			break;
		}

		return (Vector3)randPos;
	}

}
