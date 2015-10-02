using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wayPoint : MonoBehaviour {
	public Transform[] wayPoints;
	public float initialMoveSpeed = 2;
	private float moveSpeed;
	private int currentPoint;
	private int lastPoint;
	public Vector3 target;
	public Vector3 moveDirection;
	
	public bool moveBack;
	public bool Combat = false;
	public bool patrolling = true;
	public GameObject player;

	public string wpName;


	void Start ()
	{
		moveSpeed = initialMoveSpeed;
		//
		currentPoint = 0;
		moveBack = false;

		GameObject tWayPoint = GameObject.Find (wpName);
		//waypoint init stuff
		wayPoints = new Transform[tWayPoint.transform.childCount];
		for (int i = 0; i < tWayPoint.transform.childCount; i++) {
			wayPoints[i] = tWayPoint.transform.GetChild(i);
		}

		transform.position = wayPoints [0].position;

		player = GameObject.Find ("player");
	}

	void Update()
	{
		if (Combat)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		else if(patrolling)
			Patrol ();
	}



	void CheckDirection()
	{
		target = wayPoints[currentPoint].position;
		moveDirection = target - transform.position;
		
		if (moveDirection.y > 0) {
			if (GameObject.FindWithTag ("enemy")) 
			{
				GetComponent<Animator> ().SetInteger ("enemyType", 4);
	
			}
		}
		if (moveDirection.y < 0) {
			if (GameObject.FindWithTag ("enemy")) 
			{
				GetComponent<Animator> ().SetInteger ("enemyType", 3);
		
			}
		}
		if (moveDirection.x > 0) {
			if (GameObject.FindWithTag ("enemy")) 
			{
				GetComponent<Animator> ().SetInteger ("enemyType", 2);
		
			
			}
		}
		if (moveDirection.x < 0) {
			if (GameObject.FindWithTag ("enemy")) 
			{
				GetComponent<Animator> ().SetInteger ("enemyType", 1);

			}
		}
	}

	void Patrol()
	{
		if (transform.position == wayPoints [currentPoint].position && moveBack == false) {
			currentPoint++;
		}
		if (currentPoint >= wayPoints.Length) {	
			moveBack = true;
			currentPoint = wayPoints.Length - 2;
		}
		if (transform.position == wayPoints [currentPoint].position && moveBack == true) {
			currentPoint--;
			if (currentPoint < 0) {
				currentPoint = 0;
				moveBack = false;
			}
		}
		transform.position = Vector2.MoveTowards (transform.position, wayPoints [currentPoint].position, moveSpeed * Time.deltaTime);

	}


	public void spareButton()
	{
		moveSpeed = 2;


	}
}

