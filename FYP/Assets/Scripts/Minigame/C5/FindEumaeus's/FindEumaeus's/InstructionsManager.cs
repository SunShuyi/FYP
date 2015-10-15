using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum E_Instruct_Type
{
	INSTRUCT_WOLF,
	INSTRUCT_MEAT,
	INSTRUCT_TOTAL
}

public class InstructionsManager : MonoBehaviour 
{
	// ===　Singleton Structure === //
	protected static InstructionsManager m_Instance;
	public static InstructionsManager Instance
	{
		get
		{
			if (m_Instance == null)
				Debug.Log("An instance of " + typeof(InstructionsManager) + " is needed but there is none.");
			return m_Instance;
		}
	}

	// === Variables === //
	#region Variables
	public InstructionFE Instruct;
	public static bool b_IsDone = false;
	static Dictionary<E_Instruct_Type, bool> FlagCheckList = new Dictionary<E_Instruct_Type, bool> ();
	#endregion

	// === Create Instruction === //
	public void CreateInstruction(E_Instruct_Type Type)
	{
		if (Instruct == null || FlagCheckList[Type])
			return;

		Instruct.gameObject.SetActive(true);
		FlagCheckList [Type] = true;

		switch (Type)
		{
		case E_Instruct_Type.INSTRUCT_MEAT:
			Instruct.SetText("This is a tasty looking meat! Eating this will regain my health point.");
			break;
		case E_Instruct_Type.INSTRUCT_WOLF:
         	Instruct.SetText("It's a wolf! I think I better run away from it");
			break;
		default: break;
		}
	}

	// === Check if Dialogue is Closed === //
	IEnumerator CRT_CheckFlag()
	{
		while (this.gameObject.activeSelf)
		{
			b_IsDone = !Instruct.gameObject.activeSelf;
			yield return null;
		}
	}

	// === Initialisation === //
	void Start () 
	{
		// -- Populate List
		for (ushort i = 0; i < (ushort)E_Instruct_Type.INSTRUCT_TOTAL; ++i)
			FlagCheckList.Add ((E_Instruct_Type)i, false);

		m_Instance = this;
		StartCoroutine (CRT_CheckFlag ());
	}
}
