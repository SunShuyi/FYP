using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class C_Input : ScriptableObject//	: MonoBehaviour
{
	#region Singleton Structure

	private static C_Input _instance;
	private C_Input(){}
	
	public static C_Input getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Input)FindObjectOfType(typeof(C_Input));
				if(_instance == null)
					_instance = C_Input.CreateInstance<C_Input>();
					//_instance = new C_Input();
				//DontDestroyOnLoad(_instance);
			}
			return _instance;
		}
	}
	
	#endregion
	
	public bool I_Down				= false;
	public bool I_Up				= false;
	public bool I_Hold				= false;
	public bool I_Move				= false;
	
	// May change to Vector2
	public Vector3 I_Down_Position	= Vector3.zero;
	public Vector3 I_Up_Position	= Vector3.zero;
	public Vector3 I_Hold_Position	= Vector3.zero;

	// Debug Text
	public Text DebugText;

	public Vector3 I_Prev_Position	= Vector3.zero;

	void Update()
	{
		InputUpdate ();

		if(DebugText)
			DebugText.text = (Input.mousePosition).ToString();
	}

	public void InputUpdate()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN

		// If Left Mouse Down
		if(Input.GetMouseButtonDown(0))
		{
			I_Down = true;
			I_Hold = true;
			I_Down_Position = Input.mousePosition;
			I_Hold_Position = Input.mousePosition;
		}
		else I_Down = false;
		
		// If Left Mouse Hold
		if(Input.GetMouseButton(0))
		{
			I_Hold = true;
			I_Hold_Position = Input.mousePosition;
			if(I_Hold_Position != I_Prev_Position)
				I_Move = true;
			else
				I_Move = false;
		}
		else
		{
			I_Hold = false;
			I_Move = false;
		}
		
		// If Left Mouse Up
		if(Input.GetMouseButtonUp(0))
		{
			I_Up = true;
			I_Up_Position = Input.mousePosition;
		}
		else I_Up = false;

		I_Prev_Position = Input.mousePosition;

		#elif UNITY_ANDROID

		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began)
			{
				I_Down = true;
				I_Hold = true;
				I_Down_Position = touch.position;
				I_Hold_Position = touch.position;
				
			}else I_Down = false;
			
			// if still have problem, try making a threshold value for touchScreen.deltaPosition.magnitude
			if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				I_Hold_Position = touch.position;
				
				I_Move = touch.phase == TouchPhase.Moved ? true : false;
			}else I_Move = false;
			
			if(touch.phase == TouchPhase.Ended)
			{
				I_Up = true;
				I_Up_Position = touch.position;
				
				I_Hold = false;
				
			}else I_Up = false;
		}
		else
		{
			I_Up = false;
		}

		#endif

	}


}

