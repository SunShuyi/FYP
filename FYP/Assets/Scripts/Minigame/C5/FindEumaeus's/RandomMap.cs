using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomMap : MonoBehaviour {

	public GameObject Map1;
	public GameObject Map2;
	public GameObject Map3;
	public int platform;
	public EnemySpawner eSpawn;
	public EnemySpawner mSpawn;
	public EnemySpawner bSpawn;
	public EnemySpawner t1Spawn;
	public EnemySpawner t2Spawn;
	public GameObject player;
	public Vector3[] playerPos;
	public Image minimap;
	public Sprite[] levelMap;
	// Use this for initialization
	void Start () {
		Invoke ("CreateBasic", 1.5f);
	}

	void CreateBasic()
	{
		platform = Random.Range (1, 4);

		switch (platform) 
		{
		case 1: 
		{
			player.transform.localPosition = playerPos[platform-1];
			Instantiate (Map1, new Vector3 (50, 25, 0), Quaternion.identity); 
			eSpawn.enabled = true;
			eSpawn.mapVal = platform;
			mSpawn.enabled = true;
			mSpawn.mapVal = platform;
			bSpawn.enabled = true;
			bSpawn.mapVal = platform;	
			t1Spawn.enabled = true;
			t1Spawn.mapVal = platform;	
			t2Spawn.enabled = true;
			t2Spawn.mapVal = platform;
		}
				break;
		case 2:
		{
			minimap.sprite = levelMap[0];
			minimap.enabled = false;
			player.transform.localPosition = playerPos[platform-1];
			Instantiate (Map2, new Vector3 (50, 25, 0), Quaternion.identity); 
			eSpawn.enabled = true;
			eSpawn.mapVal = platform;
			mSpawn.enabled = true;
			mSpawn.mapVal = platform;
			bSpawn.enabled = true;
			bSpawn.mapVal = platform;
			t1Spawn.enabled = true;
			t1Spawn.mapVal = platform;
			t2Spawn.enabled = true;
			t2Spawn.mapVal = platform;
		}
				break;
		case 3:
		{
			minimap.sprite = levelMap[1];
			minimap.enabled = false;
			player.transform.localPosition = playerPos[platform-1];
			Instantiate (Map3, new Vector3 (50, 25, 0), Quaternion.identity); 
			eSpawn.enabled = true;
			eSpawn.mapVal = platform;
			mSpawn.enabled = true;
			mSpawn.mapVal = platform;
			bSpawn.enabled = true;
			bSpawn.mapVal = platform;
			t1Spawn.enabled = true;
			t1Spawn.mapVal = platform;
			t2Spawn.enabled = true;
			t2Spawn.mapVal = platform;
		}
				break;
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
