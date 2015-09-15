using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;

// MAY NEED TO MAKE INTO SINGLETON

// DIALOGUETREE NEEDS TO BE STATIC

public class C_DialogueManager : MonoBehaviour
{
	#region Singleton Structure
	
	private static C_DialogueManager _instance;
	private C_DialogueManager(){}
	
	public static C_DialogueManager getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_DialogueManager)FindObjectOfType(typeof(C_DialogueManager));
				if(_instance == null)
					_instance = new C_DialogueManager();
				// If Dont Destroy On Load, will cause Chapter4Manager to stay but changing scenes will have multiple Chapter4Managers
				//		Going back to previous scene will destroy the previous Chapter4Manager gameobject, causing a rewrite
				// If Destroy On Load, will cause inventory data to destroy when change scene
				//DontDestroyOnLoad(_instance);
			}
			return _instance;
		}
	}
	
	#endregion

	public TextAsset xmlFile						= null;

	public C_Chapter currentChapter			= null;
	
	public static List<C_DialogueNode> dialogueTree	= null;

	// CURRENT NODE
	[HideInInspector]
	public C_DialogueNode currentNode				= null;
	
	[HideInInspector]
	public C_DialogueNode chapterNode				= null;
	
	[HideInInspector]
	public C_DialogueNode sceneNode					= null;

	void Awake()
	{
		if (currentChapter == null)
		{
			currentChapter = gameObject.GetComponent<C_Chapter> ();
			if(!currentChapter)
				Debug.LogWarning("ChapterManager missing from DialogueManager: " + gameObject.name);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if(dialogueTree == null)
			CreateNodeTree ();

		// This needs to call on each Start
		foreach(C_DialogueNode chapter in dialogueTree)
		{
			if(Convert.ToInt32(chapter.name) == currentChapter.chapterNo)
			{
				chapterNode = chapter;

				foreach(C_DialogueNode scene in chapter.listOfChildNodes)
				{
					if(scene.name == currentChapter.scene)
					{
						sceneNode = scene;
						break;
					}
				}
				break;
			}
		}
	}

	#region Setting Up
	
	void CreateNodeTree()
	{
		dialogueTree			= new List<C_DialogueNode>();
		XmlDocument xmlDoc		= new XmlDocument();
		xmlDoc.LoadXml(xmlFile.text);
		XmlNodeList chapterList	= xmlDoc.GetElementsByTagName(C_Constants.D_E_CHAPTER);

		foreach (XmlNode chapterNode in chapterList)
		{
			// Add chapter node into dialogueTree
			C_DialogueNode tempChapterNode = new C_DialogueNode();
			tempChapterNode.dialogueType = E_DialogueType.Chapter;
			tempChapterNode.name = chapterNode.Attributes[C_Constants.D_A_NAME].Value;
			tempChapterNode.parentNode = null;

			// Create list of scenes in chapter
			XmlNodeList sceneList = chapterNode.ChildNodes;

			foreach(XmlNode sceneNode in sceneList)
			{
				if(sceneNode.NodeType != XmlNodeType.Comment)
				{
					// Add scene node into dialogueTree
					C_DialogueNode tempSceneNode = new C_DialogueNode();
					tempSceneNode.dialogueType = E_DialogueType.Scene;
					tempSceneNode.name = sceneNode.Attributes[C_Constants.D_A_NAME].Value;
					tempSceneNode.parentNode = tempChapterNode;

					// If have NPCs
					if(sceneNode.HasChildNodes)
					{

						// Create list of npcs in chapter
						XmlNodeList npcList = sceneNode.ChildNodes;

						// foreach loop again !!!
						foreach(XmlNode npcNode in npcList)
						{
							if(npcNode.NodeType != XmlNodeType.Comment)
							{
								// Add scene node into dialogueTree
								C_DialogueNode tempNpcNode = new C_DialogueNode();
								tempNpcNode.dialogueType = E_DialogueType.NPC;
								tempNpcNode.name = npcNode.Attributes[C_Constants.D_A_NAME].Value;
								tempNpcNode.parentNode = tempSceneNode;

								// nested foreach loop for the dialogue elements
								// the nested foreach loop needs to know when to break out
								AddChildNode(npcNode,tempNpcNode);

								// at end of foreach loop
								tempSceneNode.listOfChildNodes.Add(tempNpcNode);

							}
						}

					}

					// at end of foreach loop
					tempChapterNode.listOfChildNodes.Add(tempSceneNode);
				}
			}
			dialogueTree.Add(tempChapterNode);
		}

	}

	void AddChildNode( XmlNode parentNode, C_DialogueNode parentDialogueNode)
	{
		if (!parentNode.HasChildNodes)
			return;

		XmlNodeList childList = parentNode.ChildNodes;

		foreach(XmlNode childNode in childList)
		{
			// Ignore comments, maybe also text
			if(childNode.NodeType == XmlNodeType.Element) // != XmlNodeType.Comment)
			{
				// Add scene node into dialogueTree
				C_DialogueNode tempChildNode = ConvertToDialogueNode(childNode);
				tempChildNode.parentNode = parentDialogueNode;

				// Recursion here ( MAY HAVE A PROBLEM SETTING VALUES, SINCE VALUES ARE SET AFTER RECURSION )
				AddChildNode(childNode,tempChildNode);
				
				parentDialogueNode.listOfChildNodes.Add(tempChildNode);
			}
		}
	}

	C_DialogueNode ConvertToDialogueNode(XmlNode node)
	{
		C_DialogueNode convertedNode = SetNodeAttributes(node);

		switch(node.LocalName)
		{
		case C_Constants.D_E_LINE:

			convertedNode.dialogueType = E_DialogueType.Line;
			convertedNode.theText = node.InnerText;
			break;

		case C_Constants.D_E_CONDITION:
			
			convertedNode.dialogueType = E_DialogueType.Condition;
			break;

		case C_Constants.D_E_QUESTION:
			
			convertedNode.dialogueType = E_DialogueType.Question;
			break;

		case C_Constants.D_E_OPTION:
			
			convertedNode.dialogueType = E_DialogueType.Option;
			break;

		case C_Constants.D_E_OBTAIN:
			
			convertedNode.dialogueType = E_DialogueType.Obtain;
			convertedNode.theText = node.InnerText;
			break;

		case C_Constants.D_E_BACKTOQN:
			
			convertedNode.dialogueType = E_DialogueType.BackToQuestion;
			break;

		case C_Constants.D_E_CONDIALOG:
			
			convertedNode.dialogueType = E_DialogueType.ContinueDialogue;
			break;

		case C_Constants.D_E_LEAVEDIALOG:
			
			convertedNode.dialogueType = E_DialogueType.LeaveDialogue;
			break;
			
		case C_Constants.D_E_CHANGECHARACTER:
			
			convertedNode.dialogueType = E_DialogueType.ChangeCharacter;
			convertedNode.theText = node.InnerText;
			break;
			
		case C_Constants.D_E_RESULT:
			
			convertedNode.dialogueType = E_DialogueType.Result;
			convertedNode.theText = node.InnerText;
			break;

		default:

			Debug.LogWarning("Unknown Element Name in Dialogue Script > " + node.LocalName);
			break;
		}

		return convertedNode;
	}

	C_DialogueNode SetNodeAttributes(XmlNode node)
	{
		C_DialogueNode returnNode = new C_DialogueNode();

		foreach(XmlAttribute attribute in node.Attributes)
		{
			switch(attribute.LocalName)
			{
			case C_Constants.D_A_NAME:

				returnNode.name = attribute.Value;
				break;

			case C_Constants.D_A_CONDITION:
				
				returnNode.conditions = attribute.Value;
				returnNode.SetConditions();
				break;
				
			case C_Constants.D_A_QUESTION:

				returnNode.question = Convert.ToInt32(attribute.Value);
				break;
				
			case C_Constants.D_A_SENTENCE:
				
				returnNode.sentence = attribute.Value;
				break;
				
			case C_Constants.D_A_CHOICES:
				
				returnNode.choices = Convert.ToInt32(attribute.Value);
				break;
				
			default:
				
				Debug.LogWarning("Unknown Attribute Name in Dialogue Script > " + node.LocalName);
				break;
			}
		}

		return returnNode;
	}

	#endregion

	public void SetCurrentNode(string npcName)
	{
		foreach(C_DialogueNode npc in sceneNode.listOfChildNodes)
		{
			if(npc.name == npcName)
			{
				currentNode = npc.listOfChildNodes[0];
				return;
			}
		}
	}

	public bool MoveToNextNode()
	{
		if (currentNode.listOfChildNodes.Count > 0) {
			// Get first child
			currentNode = currentNode.listOfChildNodes [0];
			return true;
		} else {

			// Recursive loop may start here
			return SetNodeToNextSibling(currentNode);
		}
	}

	public bool SetNodeToNextSibling(C_DialogueNode theNode = null)
	{
		if (theNode == null)
			theNode = currentNode;

		int tempIndex = theNode.parentNode.listOfChildNodes.IndexOf(theNode);
		if(tempIndex+1 < theNode.parentNode.listOfChildNodes.Count)
		{
			// Get next sibling instead
			currentNode = theNode.parentNode.listOfChildNodes[tempIndex+1];
			return true;
		}
		else
		{
			if(theNode.parentNode.dialogueType == E_DialogueType.NPC)
				return false;

			// Get parent's next sibling
			return SetNodeToNextSibling(theNode.parentNode);
		}
	}

	public C_DialogueNode GetQuestionNode(C_DialogueNode theNode, int questionNo)
	{
		if (theNode.dialogueType == E_DialogueType.Question && theNode.question == questionNo)
		{
			return theNode;
		}
		else
		{
			if(theNode.parentNode != null)
				return GetQuestionNode(theNode.parentNode,questionNo);
			else
				return null;
		}
	}

	public C_DialogueNode GetNextSiblingNode(C_DialogueNode theNode, bool parentSibling = true)
	{
		C_DialogueNode returnNode = null;

		int tempIndex = theNode.parentNode.listOfChildNodes.IndexOf(theNode);
		if(tempIndex+1 < theNode.parentNode.listOfChildNodes.Count)
		{
			// Get next sibling instead
			returnNode = theNode.parentNode.listOfChildNodes[tempIndex+1];
			return returnNode;
		}
		else
		{
			if(theNode.parentNode.dialogueType == E_DialogueType.NPC || !parentSibling)
				return returnNode;
			
			// Get parent's next sibling
			return GetNextSiblingNode(theNode.parentNode);
		}

	}


}
