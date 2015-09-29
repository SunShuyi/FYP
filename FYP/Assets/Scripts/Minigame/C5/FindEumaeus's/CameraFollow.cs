using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.1f;
	public float speed = 3.0f;

	public float followDistance = 10f;
	public float verticalBuffer = 1.5f;
	public float horizontalBuffer = 0f;

	private Vector3 velocity = Vector3.zero;

	public Quaternion rotation = Quaternion.identity;

	public float yRotation = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = target.TransformPoint (new Vector3 (horizontalBuffer, followDistance, verticalBuffer));
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);

		transform.eulerAngles = new Vector3 (90, target.transform.eulerAngles.y, 0);
	}
}
