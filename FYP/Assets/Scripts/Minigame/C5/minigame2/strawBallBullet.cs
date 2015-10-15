using UnityEngine;
using System.Collections;

public class strawBallBullet : MonoBehaviour 
{
	public Vector3 Dir = new Vector3(0,0, 0);
	public float SpeedConstraint = 0;

	void Start () {
	}

	void Update () {
		SpeedConstraint += Time.deltaTime;
		if (SpeedConstraint >= 0.01f)
		{
			SpeedConstraint = 0;
			this.transform.position += Dir/5;
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			Destroy (this.gameObject);
	}
}
