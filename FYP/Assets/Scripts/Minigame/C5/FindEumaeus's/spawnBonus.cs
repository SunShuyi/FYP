using UnityEngine;
using System.Collections;

public class spawnBonus : MonoBehaviour {
	
	public GameObject bonus;
	
	public int numberOfDMeat = 3;
	public int minX, maxX, minY, maxY;
	
	// Use this for initialization
	void Start () 
	{
		PlaceObjects ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void PlaceObjects()
	{
		for (int i = 0; i < numberOfDMeat; i++)
		{
			Instantiate(bonus, GeneratedPosition(),Quaternion.identity);
		}
	}
	Vector3 GeneratedPosition()
	{
		int x, y;
		x = Random.Range(minX,maxX);
		y = Random.Range(minY, maxY);
		return new Vector3(x,y,0);
	}
}
