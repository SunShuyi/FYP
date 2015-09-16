using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wayPoint : MonoBehaviour {
	public Transform[] wayPoints;
	public float moveSpeed;
	private int currentPoint;
	private int lastPoint;
	public Vector3 target;
	public Vector3 moveDirection;

	public bool moveBack;



	public GameObject player;


	void Start ()
	{
	moveSpeed = 2;
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



	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Player") 
		{
			moveSpeed = 0;

		}
	}
	public void spareButton()
	{
		moveSpeed = 2;


	}
}
