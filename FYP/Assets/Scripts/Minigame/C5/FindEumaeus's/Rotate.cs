using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float xspeed = 0.0f;
	public float yspeed = 0.0f;
	public int state = 0;

	public GameObject player;
	//0 = down
	//1 = right
	//2 = up
	//3 = down
	//public float zspeed = 0.0f;

	public bool Shaking; 
	private float ShakeDecay;
	private float ShakeIntensity;    
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	// Use this for initialization
	void Start () {
		Invoke ("ObjRotate", Random.Range (1.0f, 3.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if(ShakeIntensity > 0)
		{
			transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;  
		}
	}

	public void DoShake()
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		
		ShakeIntensity = 0.3f;
		ShakeDecay = 0.02f;
		Shaking = true;
	}   
	
	public void ObjRotate()
	{

		Quaternion rotation = transform.localRotation;
		Vector3 angle = rotation.eulerAngles;
		angle.z += 90.0f;
		rotation.eulerAngles = angle;
		this.transform.localRotation = rotation;

		Quaternion rotation2 = transform.localRotation;
		Vector3 anglePlayer = rotation2.eulerAngles;
		anglePlayer.z -= 90.0f;
		rotation2.eulerAngles = angle;
		player.transform.localRotation = rotation;


//		Vector3 temp = transform.position;
//		temp.z = anglePlayer.z;
//		player.transform.position = temp;

		if (state < 3)
		{
			state += 1;
		}
		else 
			state = 0;
		Invoke ("ObjRotate", Random.Range (1.0f, 3.0f));
		DoShake();

	}
}