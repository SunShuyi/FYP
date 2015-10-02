using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public int mapVal;
	public Vector3[] Map1Pos;
	public Vector3[] Map2Pos;
	public Vector3[] Map3Pos;
	public string[] wayPoint1;
	public string[] wayPoint2;
	public string[] wayPoint3;
	public GameObject Object;
	//public Transform playerTransform;
	public GameObject[] Array;
	public wayPoint wp;
	// Use this for initialization
	void Start () {
		switch (mapVal) {
		case 1:
		{
			Array = new GameObject[Map1Pos.Length];
			for (int i = 0; i < Map1Pos.Length; i++) {
				Array[i] = Instantiate(Object,this.transform.position,Quaternion.identity) as GameObject;
				//GameObject tempTransform = Instantiate(wayPoint1[i],wayPoint1[i].transform.position,Quaternion.identity) as GameObject;
				//tempTransform.name = "waypoint";
				Array[i].transform.parent = this.transform;
				Array[i].transform.localScale = new Vector3(1,1,1);
				Array[i].transform.localPosition = Map1Pos[i];
				//tempTransform.transform.parent = Array[i].transform;
				if(wp != null)
				wp.wpName = wayPoint1[i];
			}
			break;
		}
		case 2:
		{
			Array = new GameObject[Map2Pos.Length];
			for (int i = 0; i < Map2Pos.Length; i++) {
				Array[i] = Instantiate(Object,this.transform.position,Quaternion.identity) as GameObject;
				//GameObject tempTransform = Instantiate(wayPoint2[i],wayPoint1[i].transform.position,Quaternion.identity) as GameObject;
				//tempTransform.name = "waypoint";
				Array[i].transform.parent = this.transform;
				Array[i].transform.localScale = new Vector3(1,1,1);
				Array[i].transform.localPosition = Map2Pos[i];
				//tempTransform.transform.parent = Array[i].transform;
				if(wp != null)
				wp.wpName = wayPoint2[i];
			}
			break;
		}
		case 3:
		{
			Array = new GameObject[Map3Pos.Length];
			for (int i = 0; i < Map3Pos.Length; i++) {
				Array[i] = Instantiate(Object,this.transform.position,Quaternion.identity) as GameObject;
				//GameObject tempTransform = Instantiate(wayPoint3[i],wayPoint1[i].transform.position,Quaternion.identity) as GameObject;
				//tempTransform.name = "waypoint";
				Array[i].transform.parent = this.transform;
				Array[i].transform.localScale = new Vector3(1,1,1);
				Array[i].transform.localPosition = Map3Pos[i];
				//tempTransform.transform.parent = Array[i].transform;
				if(wp != null)
				wp.wpName = wayPoint3[i];
			}
			break;
		}
		}

	}
	
	// Update is called once per frame
	void Update () {
	}
}
