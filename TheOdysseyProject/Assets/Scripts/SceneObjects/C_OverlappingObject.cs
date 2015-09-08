using UnityEngine;
using System.Collections;

public class C_OverlappingObject : MonoBehaviour
{
	//public int layerOrder				= 1;
	public Collider2D _objectBase		= null;

	private Collider2D _playerFeet		= null;
	private Renderer _objectRenderer	= null;
	private int _playerOrder			= 0;
	private bool _above					= false;
	//private float _distance				= 1.0f;

	// Use this for initialization
	void Start ()
	{
		if (_objectBase == null)
		{
			_objectBase = gameObject.GetComponent<Collider2D>();
			if (_objectBase == null)
				Debug.LogWarning ("Collider2D missing from OverlappingObject " + gameObject.name);
		}

		GameObject player = GameObject.FindWithTag ("Player");
		_playerFeet = player.GetComponent<Collider2D>();
		if (_playerFeet == null)
			Debug.LogWarning ("Collider from gameObject tagged <Player> missing from OverlappingObject " + gameObject.name);
		
		_objectRenderer = gameObject.GetComponent<Renderer>();
		if (_objectRenderer == null)
			Debug.LogWarning ("Renderer missing from OverlappingObject " + gameObject.name);
		
		_playerOrder = player.GetComponent<Renderer>().sortingOrder;
		if (_playerOrder == 0)
			Debug.LogWarning ("Renderer from gameObject tagged <Player> missing from OverlappingObject " + gameObject.name);

		if(_playerFeet.bounds.center.y < _objectBase.bounds.center.y)
		{
			//gameObject.transform.SetPositionZ(_playerFeet.gameObject.transform.position.z + _distance - layerOrder*0.2f);
			_objectRenderer.sortingOrder = _playerOrder - 1;
			_above = true;
		}
		else
			_objectRenderer.sortingOrder = _playerOrder + 1;
			//gameObject.transform.SetPositionZ(_playerFeet.gameObject.transform.position.z - _distance - layerOrder*0.2f);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_above)
		{
			if (_playerFeet.bounds.center.y > _objectBase.bounds.center.y)
			{
				_objectRenderer.sortingOrder = _playerOrder + 1;
				//gameObject.transform.SetPositionZ(_playerFeet.gameObject.transform.position.z - _distance - layerOrder*0.2f);
				_above = false;
			}
		}
		else
		{
			if (_playerFeet.bounds.center.y < _objectBase.bounds.center.y)
			{
				_objectRenderer.sortingOrder = _playerOrder - 1;
				//gameObject.transform.SetPositionZ(_playerFeet.gameObject.transform.position.z + _distance - layerOrder*0.2f);
				_above = true;
			}
		}

	}
}
