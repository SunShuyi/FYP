using UnityEngine;
using System.Collections;

// static class ??
public class C_ChapterManager : MonoBehaviour
{
	#region Singleton Structure
	
	private static C_ChapterManager _instance;
	private C_ChapterManager(){}
	
	public static C_ChapterManager getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_ChapterManager)FindObjectOfType(typeof(C_ChapterManager));
				if(_instance == null)
					_instance = new C_ChapterManager();
			}
			return _instance;
		}
	}
	
	#endregion

	public static C_Chapter currentChapter = null;

	void Awake()
	{
		if (currentChapter == null)
		{
			currentChapter = gameObject.GetComponent<C_Chapter> ().Instance ();
			if (currentChapter == null)
				Debug.LogWarning("currentChapter is NULL");
		}
	}

	public void ResetCurrentChapter()
	{
		currentChapter.DeleteChapter ();
		currentChapter = null;
	}

}
