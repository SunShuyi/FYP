using UnityEngine;
using System.Collections;

public class EmemyWaypoint : MonoBehaviour {

	public Transform[] wayPoints;
	public float moveSpeed;
	private int currentPoint;
	private int lastPoint;
	public Vector3 target;
	public Vector3 moveDirection;
	public bool moveBack;
	public GameObject player;
	public float energyReq = 25;


	//public float moveVal = -0.005f;
	
	
	void Start ()
	{
		//moveSpeed = 1;
		transform.position = wayPoints [0].position;
		currentPoint = 0;
		moveBack = false;
	}
	

	void Update()
	{
		if (transform.position == wayPoints [currentPoint].position && moveBack == false) {
			currentPoint++;
		}
		if (currentPoint >= wayPoints.Length)
		{	
			moveBack = true;
			currentPoint = wayPoints.Length-2;
		}
		if (transform.position == wayPoints [currentPoint].position && moveBack == true) 
		{
			currentPoint--;
			if(currentPoint < 0)
			{
				currentPoint = 0;
				moveBack = false;
			}
		}
		transform.position = Vector2.MoveTowards (transform.position, wayPoints [currentPoint].position, moveSpeed * Time.deltaTime);
		
	}
	
	
	
	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.CompareTag ("Player")) 
		{
			
			if (coll.gameObject.GetComponent<C_Ship> ().sliderValue > energyReq)
				Destroy (this.gameObject);
		}
	}
	
	
	void OnTriggerExit2D(Collider2D coll){
		
		if (coll.CompareTag ("Player")) 
		{
		
		
	   }
	

   }

}


