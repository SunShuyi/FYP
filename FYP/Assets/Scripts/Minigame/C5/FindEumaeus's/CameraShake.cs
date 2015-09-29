using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
//	public bool shakePosition;
//	public bool shakeRotation;
//
//	public float shakeIntensity = 0.5f;
//	public float shakeDecay = 0.02f;
//
//	private Vector3 OriginalPos;
//	private Quaternion OriginalRot;
//
//	private bool isShakeRunning = false;
//
//	public void DoShake()
//	{
//		OriginalPos = Transform.position;
//		OriginalRot = Transform.rotation;
//
//		StartCoroutine ("ProcessShake");
//	}
//	IEnumerator ProcessShake()
//	{
//		if (!isShakeRunning) {
//			isShakeRunning = true;
//			float currentShakeIntensity = shakeIntensity;
//
//			while (currentShakeIntensity > 0) {
//				if (shakePosition) {
//					transform.position = OriginalPos + Random.insideUnitSphere * currentShakeIntensity;
//				}
//				if (shakeRotation) {
//					transform.rotation = new Quaternion (OriginalRot.x + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
//					                                    OriginalRot.y + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
//					                                    OriginalRot.z + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
//					                                    OriginalRot.x + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f);
//				}
//				currentShakeIntensity -= shakeDecay;
//				yield return null;
//			}
//			isShakeRunning = false;
//		}
//	}
	public bool Shaking; 
	private float ShakeDecay;
	private float ShakeIntensity;    
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	void Start()
	{
		Shaking = false;   
	}

	// Update is called once per frame
	void Update () 
	{
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
		
	void OnGUI() {
		
		if (GUI.Button(new Rect(10, 200, 50, 30), "Shake"))
			DoShake();
		//Debug.Log("Shake");
		
	}        

	public void DoShake()
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		
		ShakeIntensity = 0.3f;
		ShakeDecay = 0.02f;
		Shaking = true;
	}    
}
