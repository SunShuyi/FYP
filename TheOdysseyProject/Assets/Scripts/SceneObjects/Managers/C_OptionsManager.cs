using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_OptionsManager : MonoBehaviour
{
	public Slider volumeSlider		= null;

	void Start()
	{
		volumeSlider.value = AudioListener.volume;
	}

	#region OnClick Functions

	public void EnglishButton()
	{

	}
	
	public void DutchButton()
	{
		
	}
	
	public void ResetButton()
	{
		volumeSlider.value = volumeSlider.maxValue;
		AudioListener.volume = volumeSlider.value;
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
