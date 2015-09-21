﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float xspeed = 0.0f;
	public float yspeed = 0.0f;
	//public float zspeed = 0.0f;
	
	// Use this for initialization
	void Start () {
		Invoke ("ObjRotate", Random.Range (1.0f, 3.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void ObjRotate()
	{
		Quaternion rotation = transform.localRotation;
		Vector3 angle = rotation.eulerAngles;
		angle.z += 90.0f;
		rotation.eulerAngles = angle;
		transform.localRotation = rotation;
		Invoke ("ObjRotate", Random.Range (1.0f, 3.0f));
	}
}