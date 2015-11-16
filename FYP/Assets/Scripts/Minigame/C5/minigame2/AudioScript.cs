using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {
	
	static AudioScript instance;
	
	public AudioClip[] sList;
	
	void Awake()
	{
		if (instance)
		{
			Destroy (gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
	
	void Start()
	{
	}
	
	void Update ()
	{

	}
	
	void playSong (int songName, bool isLooping = true) // array list with songs - scene dependent(play depend on scene)
	{
		if (GetComponent<AudioSource> ().clip != sList [songName] ) //check for songs played at different scenes
		{
			GetComponent<AudioSource> ().clip = sList [songName];
			GetComponent<AudioSource> ().loop = isLooping;
			GetComponent<AudioSource> ().Play ();
		}
	}
	
	public void playOnce (int soundName) // array list with sounds
	{
		GetComponent<AudioSource> ().PlayOneShot (sList [soundName], PlayerPrefs.GetFloat ("effect_Volume"));
	}                                //play sound one shot
	public void playOnceCustom (int soundName) // array list with sounds
	{
		GetComponent<AudioSource> ().PlayOneShot (sList[soundName]);
	}                                //play sound one shot
	
}
