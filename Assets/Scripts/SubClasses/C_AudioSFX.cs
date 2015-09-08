using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class C_AudioSFX : MonoBehaviour {

	public AudioClip[] audioSFX;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	void PlaySFX (int sFX)
	{
		audio.PlayOneShot (audioSFX [sFX]);
	}
}
