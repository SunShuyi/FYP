using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_OptionsManager : MonoBehaviour
{
	public RectTransform LangIndicator;
	public RectTransform EngPos;
	public RectTransform DutchPos;
	public Slider volumeSlider		= null;

	void Start()
	{
		if (PlayerPrefs.GetInt ("Language") == 0)//0 is uninitalized
			PlayerPrefs.SetInt ("Language", 2);
		else if (PlayerPrefs.GetInt ("Language") == 1) {
			LangIndicator.SetPositionY (EngPos.position.y);
		}
		else if (PlayerPrefs.GetInt ("Language") == 2) {
			LangIndicator.SetPositionY (DutchPos.position.y);
		}
		volumeSlider.value = AudioListener.volume;
	}

	#region OnClick Functions

	public void EnglishButton()
	{
		PlayerPrefs.SetInt ("Language", 1);//English is 1
		LangIndicator.SetPositionY (EngPos.position.y);
	}
	
	public void DutchButton()
	{
		PlayerPrefs.SetInt ("Language", 2);//Dutch is 2
		LangIndicator.SetPositionY (DutchPos.position.y);
	}
	
	public void ResetButton()
	{
		volumeSlider.value = volumeSlider.maxValue;
		AudioListener.volume = volumeSlider.value;
		PlayerPrefs.SetInt ("Language", 1);//English is 1
		LangIndicator.SetPositionY (EngPos.position.y);
	}
	
	public void BackButton()
	{
		Application.LoadLevel ("MainMenu");
	}
	
	public void ChangeVolume()
	{
		AudioListener.volume = volumeSlider.value;
	}

	#endregion

}
