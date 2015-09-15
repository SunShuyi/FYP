using UnityEngine;
using System.Collections;

public class C_C3_MG2_Ghost : MonoBehaviour
{
	public Transform sheep;
	public float speed			= 5.0f;
	public float minDistance	= 1.0f;
	bool hasDrank				= false;

	private GameObject _blood	= null;

	void Start()
	{
		_blood = gameObject.transform.GetChild (0).gameObject;
	}

	void Update()
	{
		float step = speed * Time.deltaTime;
		if(Vector3.Distance(transform.position, sheep.position) >= minDistance)
			transform.position = Vector3.MoveTowards(transform.position, sheep.position, step);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform == sheep) {
			hasDrank = true;
			_blood.SetActive(true);
			C_C3_MG2_Manager.instance.ghostsDrinking += 1;
		}
		else
		{
			if(hasDrank)
				C_C3_MG2_Manager.instance.ghostsDrinking -= 1;
			Destroy (this.gameObject);
		}
	}

}
