using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {


	void Start () {
		Invoke ("destroyArrow", 0.8f);
	}

	void destroyArrow()
	{

		Destroy (this.gameObject);

	}


		

}
