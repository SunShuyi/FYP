using UnityEngine;
using System.Collections;

public class RandomMap : MonoBehaviour {

	public GameObject Map1;
	public GameObject Map2;
	public GameObject Map3;
	public int platform;

	// Use this for initialization
	void Start () {
		Invoke ("CreateBasic", 1.5f);
	}

	void CreateBasic()
	{
		platform = Random.Range (1, 3);

		switch (platform) 
		{
		case 1: 
				Instantiate (Map1, new Vector3 (50, 25, 0), Quaternion.identity); 
				break;
		case 2:
				Instantiate (Map2, new Vector3 (0, 0, 0), Quaternion.identity); 
				break;
		case 3:
				Instantiate (Map3, new Vector3 (0, 0, 0), Quaternion.identity); 
				break;
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
