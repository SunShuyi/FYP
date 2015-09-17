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


	void Start ()
	{
		moveSpeed = initialMoveSpeed;
		transform.position = wayPoints [0].position;
		currentPoint = 0;
		moveBack = false;
	}

	void Update()
	{
		if (Combat)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		else if(patrolling)
			Patrol ();
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
