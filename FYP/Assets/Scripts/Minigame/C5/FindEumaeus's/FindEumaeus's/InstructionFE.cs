using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionFE : MonoBehaviour 
{
	// === Manage Instructions === //
	IEnumerator CRT_Manage()
	{
		while (this.gameObject.activeSelf)
		{
			if (Input.GetMouseButtonDown(0))
				this.gameObject.SetActive(false);

			yield return null;
		}
	}

	// === Set Text === //
	public void SetText(string Text)
	{
		if (this.GetComponentInChildren<Text> () != null)
			this.GetComponentInChildren<Text> ().text = Text;
	}

	// === Initialisation === //
	void Start () 
	{
		StartCoroutine (CRT_Manage ());
	}
}
