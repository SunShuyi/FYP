using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_C3_MG3_Tile : MonoBehaviour
{
	public enum E_TileType
	{
		Lava,
		Breakable,
		Explosion
	}

	public E_TileType type				= E_TileType.Breakable;

	[Header("Variables for Breakable type")]

	[Header("Time ( in seconds )")]
	public float timeBeforeBreakCheck	= 0.3f;
	public float timeBeforeBreaking		= 2.0f;
	public float durationOfExplosion	= 1.0f;

	[Header("Chance to break ( 1 / breakChance )")]
	public int breakChance				= 40;

	private Image _image				= null;
	private bool _coroutineStarted		= false;

	// Use this for initialization
	void Start ()
	{
		_image = gameObject.GetComponent<Image>();
	}

	void Update()
	{
		if(!_coroutineStarted)
		{
			if(!C_C3_MG3_Player.instance.showInstructions)
			{
				if(type == E_TileType.Breakable)
					StartCoroutine (CheckBreak());
				_coroutineStarted = true;
			}
		}
	}

	IEnumerator CheckBreak()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBeforeBreakCheck);
			
			// Chance to break
			if(Random.Range(0,breakChance) == 0)
			{
				_image.sprite = C_C3_MG3_Player.instance.tileSprite_Breaking;
				yield return StartCoroutine(DoBreak());
				_image.sprite = C_C3_MG3_Player.instance.tileSprite_Breakable;
			}
		}
	}
	
	IEnumerator DoBreak()
	{
		yield return new WaitForSeconds(timeBeforeBreaking);
		
		// Do stuff

		// Instantiate Explosion prefab here
		GameObject explosionClone = (GameObject) Instantiate (C_C3_MG3_Player.instance.explosionPrefab, transform.position,Quaternion.identity);
		
		explosionClone.transform.SetParent (transform);//.parent);
		explosionClone.transform.localScale = new Vector3(1,1,1);
		
		yield return new WaitForSeconds(durationOfExplosion);
		
		// Delete Explosion clone here
		Destroy (explosionClone);

	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (type == E_TileType.Explosion)
		{
			C_C3_MG3_Player.instance.fireDebuffIcon.fillAmount = 1;
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(type == E_TileType.Lava)
		{
			if(other.gameObject == C_C3_MG3_Player.instance.gameObject)
			{
				C_C3_MG3_Player.instance.fireDebuffIcon.fillAmount = 1;
				C_C3_MG3_Player.instance.onLava = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (type == E_TileType.Lava)
		{
			C_C3_MG3_Player.instance.onLava = false;
		}
	}

}
