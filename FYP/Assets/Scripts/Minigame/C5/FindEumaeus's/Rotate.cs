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
	public bool ReadyToRotate;
	private float ShakeDecay, ShakeDecayValue;
	private float ShakeIntensity, ShakeIntensityValue;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;
	private int SkipFrame, SkipFrameCount;
	private float MinimumRotateDuration, MaximumRotateDuration;

	// Use this for initialization
	void Start () {
		MinimumRotateDuration = 2.0f;
		MaximumRotateDuration = 4.0f;
		ShakeIntensity = ShakeIntensityValue = 0.15f;
		ShakeDecay = ShakeDecayValue = 0.015f;
		SkipFrame = SkipFrameCount = 5;

		Shaking = false;
		ReadyToRotate = false;

		Invoke ("DoShake", Random.Range (MinimumRotateDuration, MaximumRotateDuration));
	}
	
	// Update is called once per frame
	void Update () {
		if (0 == (SkipFrame--))
		{
			if ((true == Shaking) && (0 < ShakeIntensity)) {
				transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
				transform.rotation = new Quaternion (OriginalRot.x + Random.Range (-ShakeIntensity, ShakeIntensity) * .2f,
				                                     OriginalRot.y + Random.Range (-ShakeIntensity, ShakeIntensity) * .2f,
				                                     OriginalRot.z + Random.Range (-ShakeIntensity, ShakeIntensity) * .2f,
				                                     OriginalRot.w + Random.Range (-ShakeIntensity, ShakeIntensity) * .2f);
				
				ShakeIntensity -= ShakeDecay;
			}
			else if ((true == Shaking) && (0 >= ShakeIntensity))
			{
				Shaking = false;
				ReadyToRotate = true;
			}
			SkipFrame = SkipFrameCount;
		}
		if ( true == ReadyToRotate )
		{
			Invoke("ObjRotate", 0.5f);
			ReadyToRotate = false;
		}
	}

	public void DoShake()
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		
		ShakeIntensity = ShakeIntensityValue;
		ShakeDecay = ShakeDecayValue;
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

		ReadyToRotate = false;
		Invoke ("DoShake", Random.Range (MinimumRotateDuration, MaximumRotateDuration));	
	}
}