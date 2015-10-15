using UnityEngine;
using System.Collections;

public class C_Fade : MonoBehaviour {

	public Texture2D fadeOutTex;
	public float fadeSpd = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;
	public bool byTime = true;
	void OnGUI () {
		if (byTime)
			alpha += fadeDir * fadeSpd * Time.deltaTime;
		else
			alpha += fadeDir * fadeSpd * 0.01f;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); 
		GUI.depth = drawDepth;
		GUI.DrawTexture( new Rect (0, 0, Screen.width, Screen.height), fadeOutTex );
	}

	public float BeganFade (int direction) {
		fadeDir = direction;
		return (fadeSpd);
	}

	void OnLevelWasLoaded () {
		BeganFade (-1);
	}
}
