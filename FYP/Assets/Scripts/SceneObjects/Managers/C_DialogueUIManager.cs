using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C_DialogueUIManager : MonoBehaviour
{
	public Text characterName						= null;
	// Image MAY have an animator with states, eg. talking, idle states
	public Image characterImageLeft					= null;
	public Image characterImageRight				= null;
	[HideInInspector]
	public Animator characterAnimatorLeft			= null;
	[HideInInspector]
	public Animator characterAnimatorRight			= null;

	private bool _startTalking						= false;
	private bool _isTalking							= false;
	[HideInInspector]
	public E_HorizontalDirection talkingCharacter	= E_HorizontalDirection.None;

	[HideInInspector]
	public string characterNameLeft					= "";
	[HideInInspector]
	public string characterNameRight				= "";

	[Header("Settings")]
	public Color fadedColor							= Color.gray;

	public List<C_DialogueBox> dialogueBoxes		= new List<C_DialogueBox>(3);
	[HideInInspector]
	public C_DialogueBox currentDialogueBox			= null;
	[HideInInspector]
	public bool isQuestion							= false;

	void Start()
	{
		characterAnimatorLeft = characterImageLeft.gameObject.GetComponent<Animator> ();
		characterAnimatorRight = characterImageRight.gameObject.GetComponent<Animator> ();
		currentDialogueBox = dialogueBoxes [0];
	}

	// Update is called once per frame
	void Update ()
	{
		if (talkingCharacter == E_HorizontalDirection.Left)
		{
			SetImageFade(characterImageLeft,false);
			SetImageFade(characterImageRight,true);
			if(!_isTalking && !_startTalking)
				_startTalking = true;
			else
				_startTalking = false;
			_isTalking = true;
		}
		else if (talkingCharacter == E_HorizontalDirection.Right)
		{
			SetImageFade(characterImageLeft,true);
			SetImageFade(characterImageRight,false);
			if(!_isTalking && !_startTalking)
				_startTalking = true;
			else
				_startTalking = false;
			_isTalking = true;
		}
		else if (talkingCharacter == E_HorizontalDirection.None)
		{
			SetImageFade(characterImageLeft,false);
			SetImageFade(characterImageRight,false);
			if(_isTalking)
			{
				characterAnimatorLeft.SetBool("isTalking",false);
				if(characterAnimatorRight.runtimeAnimatorController != null)
					characterAnimatorRight.SetBool("isTalking",false);
			}
			_startTalking = false;
			_isTalking = false;
		}

		// BAD CODING
		if (_isTalking)// && _startTalking)
		{
			//Debug.Log("\tstart talking");
			if(talkingCharacter == E_HorizontalDirection.Left)
			{
				characterName.text = characterNameLeft;
				characterAnimatorLeft.SetBool ("isTalking", true);
				if(characterAnimatorRight.runtimeAnimatorController != null)
					characterAnimatorRight.SetBool("isTalking",false);
			}
			else if(talkingCharacter == E_HorizontalDirection.Right)
			{
				characterName.text = characterNameRight;
				if(characterAnimatorRight.runtimeAnimatorController != null)
					characterAnimatorRight.SetBool ("isTalking", true);
				characterAnimatorLeft.SetBool("isTalking",false);
			}
		}
	}

	void SetImageFade(Image theImage, bool fade)
	{
		if (fade)
		{
			if(theImage.color != fadedColor)
				theImage.color = fadedColor;
		}
		else
		{
			if(theImage.color != Color.white)
				theImage.color = Color.white;
		}
	}

	public void ChangeDialogueBox(int index)
	{
		if (index == 0)
			isQuestion = false;
		else
			isQuestion = true;

		currentDialogueBox = dialogueBoxes [index];
		
		foreach(C_DialogueBox dBox in dialogueBoxes)
		{
			if(dBox == currentDialogueBox)
				dBox.gameObject.SetActive(true);
			else
				dBox.gameObject.SetActive(false);
		}

	}

}
