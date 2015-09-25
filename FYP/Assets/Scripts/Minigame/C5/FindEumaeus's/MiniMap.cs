using UnityEngine;
using System.Collections;

//0 references

public class MiniMap : MonoBehaviour {



	public Transform Target;
	public float ZoomLevel= 10f;

	//0 references
	public Vector2 TransformPosition(Vector3 position)
	{
		Vector3 offset = position - Target.position;
		Vector2 newPosition = new Vector2 (offset.x, offset.y);
		newPosition *= ZoomLevel;
		return newPosition;
	}

	// Use this for initializations
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
