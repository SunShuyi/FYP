using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_C3_MG4_Ghost : MonoBehaviour
{
	public Vector3 finalScale		= new Vector3();
	public Color finalColor			= new Color();
	private float _delayTime		= 4.0f;
	private float _speed			= 10.0f;
	private Vector2 _destination;
	private BoxCollider2D _region	= null;

	private Vector3 _initialPos;
	private Vector3 _initialScale;
	private Color _initialColor;
	
	[HideInInspector]
	public Collider2D collider		= null;
	[HideInInspector]
	public bool appeared			= false;
	private C_Timer _appearTimer	= null;

	void Start ()
	{
		_initialPos = gameObject.transform.position;
		_initialScale = gameObject.transform.localScale;
		_initialColor = gameObject.GetComponent<Image>().color;

		collider = gameObject.GetComponent<Collider2D>();
		_region = gameObject.transform.parent.GetComponent<BoxCollider2D>();

		_speed = Random.Range (5.0f,15.0f);
		_delayTime = Random.Range (4.0f,10.0f);
		_destination = new Vector2 (Random.Range(_region.bounds.min.x,_region.bounds.max.x),
		                            Random.Range(_region.bounds.min.y,_region.bounds.max.y));
		
		_appearTimer = new C_Timer (E_TimerType.Countdown, 0, _delayTime);

		//Debug.Log ("Destination\t" + _destination.ToString());
		//Debug.DrawLine (transform.position,_destination,Color.red,5f);
	}

	public void ResetGhost()
	{
		gameObject.transform.position = _initialPos;
		gameObject.transform.localScale = _initialScale;
		gameObject.GetComponent<Image>().color = _initialColor;
		_speed = Random.Range (5.0f,15.0f);
		_delayTime = Random.Range (4.0f,10.0f);
		_destination = new Vector2 (Random.Range(_region.bounds.min.x,_region.bounds.max.x),
		                            Random.Range(_region.bounds.min.y,_region.bounds.max.y));

		_appearTimer.Reset(true,E_TimerType.Countdown,0,_delayTime);
		appeared = false;
	}

	void UpdateMovement()
	{
		float step = _speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, _destination, step);
		float distanceFromStart = Vector3.Distance (_initialPos, transform.position);
		float fullDistance = Vector3.Distance (_initialPos, _destination);
		transform.localScale = Vector3.Lerp (_initialScale, finalScale, (distanceFromStart / fullDistance));

		if (_appearTimer.UpdateTimer ())
		{
			appeared = true;
			gameObject.GetComponent<Image>().color = finalColor;
		}
	}

	void Update()
	{
		UpdateMovement ();
	}

}
