using UnityEngine;
using System.Collections;

public class ShootStrawBall : MonoBehaviour 
{
	public float coolDown;
	public GameObject strawBallprefab;

	void Start () {
		coolDown = 20;
	}

	void Update () 
	{
		coolDown-=Time.deltaTime;
		if (coolDown <= 0 && strawBallprefab != null) {
			coolDown = 20;
			GameObject bullet = Instantiate (strawBallprefab, this.transform.position, Quaternion.identity) as GameObject;
			Vector2 Dir = (GameObject.Find ("theShip").transform.position - this.transform.position).normalized;
			bullet.GetComponent<strawBallBullet> ().Dir = Dir;
		}
	}
}