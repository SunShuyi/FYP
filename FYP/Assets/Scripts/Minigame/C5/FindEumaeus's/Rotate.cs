using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float xspeed = 0.0f;
	public float yspeed = 0.0f;
	public int state = 0;
	
	public GameObject player;
	public GameObject StationeryObjects;
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
		MinimumRotateDuration = 5.0f;
		MaximumRotateDuration = 15.0f;
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

	float roundOff(float value)
	{
		Vector2 val = new Vector2 (value, 0);
		Vector2[] degrees = new Vector2[5]
		{new Vector2 (0, 0),new Vector2 (90, 0),new Vector2 (180, 0),
			new Vector2 (270, 0),new Vector2 (360, 0)};
		float[] distance = new float[5];
		for (int i = 0; i < degrees.Length; i++) {
			distance[i] = Vector2.Distance (val, degrees [i]);
		}
		Vector2 nearestValue = new Vector2 (500, 0);
		for (int i = 0; i < distance.Length; i++) {
			if(nearestValue.x > distance[i])
			{
				nearestValue.x = distance[i];
				nearestValue.y = i;
			}
		}
		return degrees [(int)nearestValue.y].x;
	}
	public void ObjRotate()
	{
		
		Quaternion rotation = transform.localRotation;
		Vector3 angle = rotation.eulerAngles;
		if (angle.z >= 360)
			angle.z = 0;
		angle.z += 90.0f;
		angle.z = roundOff(angle.z);
		rotation.eulerAngles = angle;
		this.transform.localRotation = rotation;
		
		Quaternion rotation2 = transform.localRotation;
		Vector3 anglePlayer = rotation2.eulerAngles;
		if (angle.z <= -360)
			anglePlayer.z = 360;
		anglePlayer.z -= 90.0f;
		anglePlayer.z = roundOff(anglePlayer.z);
		rotation2.eulerAngles = angle;
		player.transform.localRotation = rotation;
		
		Quaternion rotation3 = transform.localRotation;
		Vector3 angleStationeryObjects = rotation2.eulerAngles;
		if (angle.z <= -360)
			angleStationeryObjects.z = 360;
		angleStationeryObjects.z -= 90.0f;
		angleStationeryObjects.z = roundOff(angleStationeryObjects.z);
		rotation3.eulerAngles = angle;
		StationeryObjects.transform.localRotation = rotation;
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