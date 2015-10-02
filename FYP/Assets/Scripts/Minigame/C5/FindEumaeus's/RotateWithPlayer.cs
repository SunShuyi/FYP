using UnityEngine;
using System.Collections;

public class RotateWithPlayer : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player").transform;
		Invoke ("Rotate", 0.2f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Rotate()
	{
		this.transform.localRotation = player.localRotation;
		Invoke ("Rotate", 0.2f);
	}
}
