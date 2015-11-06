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
			if (PlayerPrefs.GetInt ("Language") == 1)
			{
				currentChapter.conditionTriggers.Remove("isEnglish");
				currentChapter.conditionTriggers.Remove("isDutch");
				currentChapter.conditionTriggers.Add("isEnglish");
				//currentChapter.conditionTriggers.RemoveAll("isDutch");
			}
			else if (PlayerPrefs.GetInt ("Language") == 2)
			{
				currentChapter.conditionTriggers.Remove("isEnglish");
				currentChapter.conditionTriggers.Remove("isDutch");
				currentChapter.conditionTriggers.Add("isDutch");
				//currentChapter.conditionTriggers.RemoveAll("isEnglish");//RemoveAll("isEnglish");
			}
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
