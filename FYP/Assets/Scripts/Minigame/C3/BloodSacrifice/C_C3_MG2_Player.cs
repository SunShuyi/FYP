using UnityEngine;
using System.Collections;

public class C_C3_MG2_Player : MonoBehaviour
{
	public float rotationSpeed				= 50.0f;
	[HideInInspector]
	public bool canUpdate					= false;

	E_HorizontalDirection rotateDirection	= E_HorizontalDirection.None;

	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN

		if (canUpdate) {

			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Rotate (Vector3.forward * Time.deltaTime * rotationSpeed);
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Rotate (-Vector3.forward * Time.deltaTime * rotationSpeed);
			}
		}
		
		#endif

		if (rotateDirection == E_HorizontalDirection.Left) {
			transform.Rotate (Vector3.forward * Time.deltaTime * rotationSpeed);
		} else if (rotateDirection == E_HorizontalDirection.Right) {
			transform.Rotate (-Vector3.forward * Time.deltaTime * rotationSpeed);
		}
	}

#region OnClick Functions

	public void RotateButtons(bool right)
	{
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow))
			return;

		if (!right)
		{
			rotateDirection = E_HorizontalDirection.Left;
		}
		else
		{
			rotateDirection = E_HorizontalDirection.Right;
		}
	}

	public void RotateOff()
	{
		rotateDirection = E_HorizontalDirection.None;
	}

#endregion
}
