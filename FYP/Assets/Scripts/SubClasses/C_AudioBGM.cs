using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class C_AudioBGM : MonoBehaviour {

	public AudioClip[] audBGM;
	public int choice;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		PlayBGM (choice);
	}

	void PlayBGM (int currBGM)
	{
		audio.clip = audBGM [currBGM - 1];
		audio.Play ();

	}
}
