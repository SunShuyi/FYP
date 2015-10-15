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
	Animator enemyAnim;
	public Transform ship;
	public bool Chase = false;
	bool collided = false;

	
	
	void Start ()
	{
		ship = this.transform.GetChild (0).GetComponent<Transform> ();
		//moveSpeed = 1;
		transform.position = wayPoints [0].position;
		currentPoint = 0;
		moveBack = false;
		enemyAnim = GetComponent<Animator> ();


	}
	
	

	void Update()
	{



		if (!Chase && GetComponent<Rigidbody2D>().velocity == new Vector2(0,0))
			Patrol ();
		else
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		if (collided) {
			Vector2 dir = (player.transform.position - transform.position).normalized; 
			dir.y = -dir.y;
			Vector2 dir2 = (transform.position - player.transform.position  ).normalized; 
			dir2.y = -dir2.y;
			player.GetComponent<Rigidbody2D>().velocity = dir*1f;
			GetComponent<Rigidbody2D>().velocity = dir2*1f;

			if (player.GetComponent<C_Ship> ().sliderValue > energyReq) {
				player.GetComponent<C_Ship> ().sliderValue -= energyReq;
				enemyAnim.SetBool ("isDead", true);
				Destroy (this.gameObject);
				
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
		
		//rigidbody.AddRelativeForce(Vector2.Forward*GetAxis("Vertical"))*moveSpeed;
		transform.position = Vector2.MoveTowards (transform.position, wayPoints [currentPoint].position, moveSpeed * Time.deltaTime);
		Vector3 dir = (wayPoints [currentPoint].position - transform.position).normalized;
		Debug.Log (dir);
		float rot_z = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		
		ship.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);
		//transform.position = Vector2.MoveTowards (moveSpeed * Input .GetAxis ("Horizintal") * Time.deltaTime);

	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.collider.CompareTag ("Player")) 
		{
			collided = true;
			if (coll.collider.gameObject.GetComponent<C_Ship> ().sliderValue > energyReq)
			{

				coll.collider.gameObject.GetComponent<C_Ship> ().sliderValue = 0;
				Destroy (this.gameObject);
				enemyAnim.SetBool("isDead", true);
				player.GetComponent<C_Ship>().collidedShips--;
			}
			else 
			{
				player.GetComponent<C_Ship>().StrengthScript.StrengthCount--;
				player.GetComponent<C_Ship>().StrengthScriptShadow.StrengthCount--;
				if( player.GetComponent<C_Ship>().StrengthScript.StrengthCount == 0)
				{
					player.GetComponent<C_Ship>().shipAnim.SetBool ("isDead", true);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag  ("Rocks")) {
			GetComponent<Rigidbody2D>().velocity =new Vector2(0,0);
			
		}
		
		
	}
	void OnTriggerExit2D(Collider2D coll){
		
		if (coll.CompareTag ("Player")) 
		{
			collided = false;
		
	   }
	

   }

}


