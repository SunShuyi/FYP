using UnityEngine;
using System.Collections;

public class ExitIndicator : MonoBehaviour 
{
	public Collider Boundary;
	Transform Exit;

	// === Rotate Towards Exit === //
	IEnumerator CRT_Rotate()
	{
		while (this.gameObject.activeSelf)
		{
			if (Exit != null)
			{
				// Get Angle in Radians
				float AngleRad = Mathf.Atan2(Exit.position.y - this.transform.position.y, Exit.position.x - this.transform.position.x);
				// Get Angle in Degrees
				float AngleDeg = (180 / Mathf.PI) * AngleRad;
				// Rotate Object
				this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
			}

			yield return null;
		}
	}

	// === Find (-｡-;) Exit === //
	IEnumerator CRT_FindExit()
	{
		while (this.gameObject.activeSelf && Boundary != null)
		{
			if (Exit != null)
			{
				this.transform.position = Exit.position;

				// -- Clamp
				float f_PosX = this.transform.position.x,
					  f_PosY = this.transform.position.y;
				f_PosX = Mathf.Clamp(f_PosX, Boundary.transform.position.x-Boundary.bounds.size.x*0.5f, Boundary.transform.position.x+Boundary.bounds.size.x*0.5f);
				f_PosY = Mathf.Clamp(f_PosY, Boundary.transform.position.y-Boundary.bounds.size.y*0.5f, Boundary.transform.position.y+Boundary.bounds.size.y*0.5f);
				this.transform.position = new Vector3(f_PosX, f_PosY, this.transform.position.z);
			}
			else if (Exit == null && GameObject.FindGameObjectWithTag ("Exit") != null)
				Exit = GameObject.FindGameObjectWithTag ("Exit").transform;

			yield return null;
		}
	}

	// === Initialisation === //
	void Start () 
	{
		StartCoroutine (CRT_FindExit ());
		StartCoroutine (CRT_Rotate ());
	}
}
