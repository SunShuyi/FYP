using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	public float Radius = 50;
	public float Speed = 5;
	Vector2 targetPos = new Vector2(0,0);
	Vector2 Offset = new Vector2(0,0);
	public float minX = 0;
	public float maxX = 0;
	// Use this for initialization
	void Start () {
		targetPos = Random.insideUnitCircle * Radius;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKeyDown ("w")) 
//		{
//			transform.Translate (Vector3.up * Time.deltaTime * 5);
//			targetPos = targetPos + (Vector2)(Vector3.up * Time.deltaTime * 5);
//			Offset = Offset + (Vector2)(Vector3.up * Time.deltaTime * 5);
//
//		}
//		if (Input.acceleration.x > 0) {
//			if ((Input.acceleration.x + transform.position.x) <= maxX) {
//				transform.Translate (Input.acceleration.x, 0);
//				targetPos = targetPos + (Vector2)(Input.acceleration.x);
//				Offset = Offset + (Vector2)(Input.acceleration.x);
//			}
//		}
//		else if (Input.acceleration.x < 0) {
//			if ((Input.acceleration.x + transform.position.x) >= minX) {
//				transform.Translate (Input.acceleration.x, 0);
//				targetPos = targetPos + (Vector2)(Input.acceleration.x);
//				Offset = Offset + (Vector2)(Input.acceleration.x);
//			}
//		}
//
//		if (Input.acceleration.y > 0) {
//			if ((Input.acceleration.y + transform.position.y) <= maxX) {
//				transform.Translate (Input.acceleration.y, 0);
//				targetPos = targetPos + (Vector2)(Input.acceleration.y);
//				Offset = Offset + (Vector2)(Input.acceleration.y);
//			}
//		}
//		else if (Input.acceleration.y < 0) {
//			if ((Input.acceleration.y + transform.position.y) >= minX) {
//				transform.Translate (Input.acceleration.y, 0);
//				targetPos = targetPos + (Vector2)(Input.acceleration.y);
//				Offset = Offset + (Vector2)(Input.acceleration.y);
//			}
//		}


		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKeyDown ("a"))
		{
			if(((Vector3.left * Time.deltaTime *  5).x+transform.position.x) >= minX)
			{
				transform.Translate (Vector3.left * Time.deltaTime *  5); 
				targetPos = targetPos + (Vector2)(Vector3.left * Time.deltaTime * 5);
				Offset = Offset + (Vector2)(Vector3.left * Time.deltaTime * 5);
			}

		}
			
//		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKeyDown ("s"))
//		{
//			transform.Translate (Vector3.down * Time.deltaTime *  5); 
//			targetPos = targetPos + (Vector2)(Vector3.down * Time.deltaTime * 5);
//			Offset = Offset + (Vector2)(Vector3.down * Time.deltaTime * 5);
//		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKeyDown ("d"))
		{
			if(((Vector3.right * Time.deltaTime *  5).x+transform.position.x) <= maxX)
			{

				transform.Translate (Vector3.right * Time.deltaTime *  5); 
				targetPos = targetPos + (Vector2)(Vector3.right * Time.deltaTime * 5);
				Offset = Offset + (Vector2)(Vector3.right * Time.deltaTime * 5);
			}
		}

		transform.position = Vector2.MoveTowards (transform.position, targetPos, Speed*Time.smoothDeltaTime);//transform.position
		if(targetPos == (Vector2)transform.position)
			targetPos = (Random.insideUnitCircle) * Radius +Offset;
	}
}
