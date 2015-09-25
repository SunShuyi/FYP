using UnityEngine;
using System.Collections;

//0 references

public class Blip : MonoBehaviour {


	public Transform Target;
	public bool LockScale = false ;

	MiniMap map;
	RectTransform  myRectTransform;


	//0 references
	void Start () {

		map = GetComponentInParent<MiniMap>();
		myRectTransform = GetComponent<RectTransform>();
	
	}
	
	//0 references
	void LateUpdate () {

		Vector2 newPosition = map.TransformPosition (Target.position);

		if (!LockScale)

		myRectTransform.localScale = new Vector3 (map.ZoomLevel, map.ZoomLevel, 1);

		myRectTransform.localPosition = newPosition;

	
	}
}
