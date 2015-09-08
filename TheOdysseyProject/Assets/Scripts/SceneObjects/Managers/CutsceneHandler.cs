using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class CutsceneHandler : MonoBehaviour {

	C_Timer theTimer;

	public Sprite[] CutsceneArr = new Sprite[8]; 
	public int SceneCount;

	private Image theImage;

	public C_CutsceneTxt theCSText;

	public float timeSecs = 5.0f;

	public string nextScene;

	public float timeDelay = 2.0f;

	private float timeDiff;

	void Start(){
		theTimer = new C_Timer (E_TimerType.Countdown, 0.0f, timeSecs);
		SceneCount = 0;
		theImage = this.GetComponent<Image> ();
		theImage.sprite = CutsceneArr [SceneCount];
	}

	void Update(){
		C_Input.getInstance.InputUpdate ();

		timeDiff = timeSecs - timeDelay;

#if UNITY_EDITOR
		if(theTimer.UpdateTimer()){
			theTimer.Reset();
			if(SceneCount == CutsceneArr.Length - 1){
				Application.LoadLevel(nextScene);
			} else {
				SceneCount++;
				theImage.sprite = CutsceneArr [SceneCount];
				theCSText.txtCount++;
			}
		}
		
		else
		{
			if(Input.GetKeyDown("space") && theTimer.seconds < timeDiff)
			{
				theTimer.Reset();
				if(SceneCount == CutsceneArr.Length - 1){
					Application.LoadLevel(nextScene);
				} else {
					SceneCount++;
					theImage.sprite = CutsceneArr [SceneCount];
					theCSText.txtCount++;
				}
			}
		}
#else
		//if(C_Input.getInstance.I_Down || theTimer.UpdateTimer()){
		if(theTimer.UpdateTimer()){
			theTimer.Reset();
			if(SceneCount == CutsceneArr.Length - 1){
				Application.LoadLevel(nextScene);
			} else {
				SceneCount++;
				theImage.sprite = CutsceneArr [SceneCount];
				theCSText.txtCount++;
			}
		}

		else
		{
			if(C_Input.getInstance.I_Down && theTimer.seconds < timeDiff)
			{
				theTimer.Reset();
				if(SceneCount == CutsceneArr.Length - 1){
					Application.LoadLevel(nextScene);
				} else {
					SceneCount++;
					theImage.sprite = CutsceneArr [SceneCount];
					theCSText.txtCount++;
				}
			}
		}

#endif

	}
}
