using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class C_DialogueNode
{
	// may need add in:	public Sprite dialogueCharacterSprite = null;

	public E_DialogueType dialogueType				= E_DialogueType.Line;

	
	// bool also can, since need to convert
	// All the attributes
	// conditions is a List because there may be more than one
	public string name								= "";
	public string conditions						= "";
	public List<string> conditionsList				= new List<string>();
	public int question								= 0;
	public int choices								= 0;
	public string sentence							= "";
	public string resultType						= "";
	
	public string theText							= "";

	// Array of DialogueNodes under this Node
	[System.NonSerialized]
	public List<C_DialogueNode> listOfChildNodes	= new List<C_DialogueNode>();
	[System.NonSerialized]
	public C_DialogueNode parentNode				= null;

	// need a pointer to return node to after it's complete (eg. parent or start of question/condition)

	// Method to separate multiple conditions into conditionsList
	public void SetConditions()
	{
		conditionsList = new List<string>(conditions.Split (' '));
	}

}
