using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C_C2_MG1_Player : MonoBehaviour
{
	public Slider theSlider						= null;
	public C_MovingBackground obstacles			= null;
	public float topYValue						= 0.0f;
	public float midYValue						= 0.0f;
	public float botYValue						= 0.0f;
	public float normalSpeed					= 0.0f;
	public float maxSpeed						= 0.0f;
	public float slowedSpeed					= 0.0f;
	public float slowedTimeInSec				= 3.0f;
	public float fullGameLengthInSec			= 0.0f;
	public string nextSceneName					= "";

	private E_VerticalDirection _currentPos		= E_VerticalDirection.None;
	private C_Input _inputManager				= null;
	private C_Timer _slowedTimer				= null;
	private bool _slowed						= false;
	private List<Transform> _obstacleSets		= new List<Transform> ();
	private float _currentGameTime				= 0.0f;
	private bool _started						= false;

	// Use this for initialization
	void Start () {
		_inputManager = C_Input.getInstance;
		_slowedTimer = new C_Timer (E_TimerType.Countdown, 0, slowedTimeInSec);

		foreach(Transform child in obstacles.transform)
		{
			_obstacleSets.Add(child);
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		_inputManager.InputUpdate ();
		
//		if (!_started)
//		{
//			if(_inputManager.I_Up)
//			{
//				Time.timeScale = 1;
//				_started = true;
//			}
//			return;
//		}

		if (_currentGameTime < fullGameLengthInSec)
		{
			if(!_slowed)
			{
				_currentGameTime += Time.deltaTime;
				theSlider.value = _currentGameTime / fullGameLengthInSec;
			}
		}
		else
		{
			// EndGame
			if(nextSceneName != "")
				Application.LoadLevel(nextSceneName);
		}

		if (_slowed)
		{
			if(_slowedTimer.UpdateTimer ())
			{
				obstacles.horizontalSpeed = normalSpeed;
				_slowed = false;
			}
		}

		if(obstacles.resetPos)
		{
			int randNum = Random.Range(0,_obstacleSets.Count);

			if(normalSpeed+1<=maxSpeed)
				normalSpeed++;

			for(int i = 0; i < _obstacleSets.Count; i++)
			{
				if(i==randNum)
					_obstacleSets[i].gameObject.SetActive(true);
				else
					_obstacleSets[i].gameObject.SetActive(false);
			}

		}

		if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
			MovePlayer(true);

		else if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
			MovePlayer(false);

		else if(_inputManager.I_Up)
		{
			if(_inputManager.I_Up_Position.y > Screen.height/2)
				MovePlayer(true);
			else
				MovePlayer(false);
		}

	}

	void MovePlayer(bool up = true)
	{
		if (up)
		{
			if(_currentPos == E_VerticalDirection.Down)
			{
				this.transform.SetLocalPositionY(midYValue);
				_currentPos = E_VerticalDirection.None;
			}
			else if(_currentPos == E_VerticalDirection.None)
			{
				this.transform.SetLocalPositionY(topYValue);
				_currentPos = E_VerticalDirection.Up;
			}
		}
		else
		{
			if(_currentPos == E_VerticalDirection.Up)
			{
				this.transform.SetLocalPositionY(midYValue);
				_currentPos = E_VerticalDirection.None;
			}
			else if(_currentPos == E_VerticalDirection.None)
			{
				this.transform.SetLocalPositionY(botYValue);
				_currentPos = E_VerticalDirection.Down;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!_slowed)
		{
			_slowedTimer.Reset();
			obstacles.horizontalSpeed = slowedSpeed;
			_slowed = true;
		}
	}

}
