using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	public float Radius = 50;
	public float Speed = 5;
	Vector2 targetPos = new Vector2(0,0);
	Vector2 Offset = new Vector2(0,0);
	// Use this for initialization
	void Start () {
		targetPos = Random.insideUnitCircle * Radius;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKeyDown ("w")) 
		{
			transform.Translate (Vector3.up * Time.deltaTime * 25);
			targetPos = targetPos + (Vector2)(Vector3.up * Time.deltaTime * 25);
			Offset = Offset + (Vector2)(Vector3.up * Time.deltaTime * 25);
		}

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKeyDown ("a"))
		{
			transform.Translate (Vector3.left * Time.deltaTime *  25); 
			targetPos = targetPos + (Vector2)(Vector3.left * Time.deltaTime * 25);
			Offset = Offset + (Vector2)(Vector3.left * Time.deltaTime * 25);
		}
			
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKeyDown ("s"))
		{
			transform.Translate (Vector3.down * Time.deltaTime *  25); 
			targetPos = targetPos + (Vector2)(Vector3.down * Time.deltaTime * 25);
			Offset = Offset + (Vector2)(Vector3.down * Time.deltaTime * 25);
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKeyDown ("d"))
		{
			transform.Translate (Vector3.right * Time.deltaTime *  25); 
			targetPos = targetPos + (Vector2)(Vector3.right * Time.deltaTime * 25);
			Offset = Offset + (Vector2)(Vector3.right * Time.deltaTime * 25);
		}

		transform.position = Vector2.MoveTowards (transform.position, targetPos, Speed*Time.smoothDeltaTime);//transform.position
		if(targetPos == (Vector2)transform.position)
			targetPos = (Random.insideUnitCircle) * Radius +Offset;
	}
}
