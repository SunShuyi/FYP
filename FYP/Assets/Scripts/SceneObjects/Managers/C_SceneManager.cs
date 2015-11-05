using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

// Contains OnClick Button functions as well


public class C_SceneManager : MonoBehaviour
{
	#region Member Variables

	[HideInInspector]
	public C_Input theInputs					= null;
	public GameObject objects					= null;
	public GameObject items						= null;
	// May need to change initialisation method for player if its a singleton
	public C_Player player						= null;
	public GameObject characters				= null;
	public GameObject exits						= null;
	public Collider2D walkableRegion			= null;
	public C_DialogueUIManager dialogueUI		= null;
	public GameObject descriptionBox			= null;
	public GameObject pauseMenu					= null;

	// List of inactive gameobjects
	public List<GameObject> inactiveGameobjects	= new List<GameObject>();

	public C_Timer movementTimer				= new C_Timer ();

	// Movement variables
	private bool _movePlayer					= false;
	private Vector3 _targetPos					= new Vector3();
	private Vector3 _startPos					= new Vector3();
	private Vector3 _offsetPos					= new Vector3();
	private float _timeToMove					= 1.0f;
	private string _theExitScene				= "";
	private bool _showButtons					= false;
	private bool _clickButton					= false;
	private bool _pause							= false;
	private bool _skipUpdate					= false;

	// May need to change from fixed size array to list
	private C_Character[] _characters			= null;
	private C_Exit[] _exits						= null;
	private C_InWorldItem[] _items				= null;
	private C_InteractableObject[] _objects		= null;
	//private List<C_InteractableObject> _objects		= null;
	private int _itemClickedID					= -1;
	private int _objectClickedID				= -1;
	private int _npcClickedID					= -1;
	private string _selectedItem				= "";

	// Dialogue
	private bool _dialogueEnabled				= false;
	private bool _dialogueQuestion				= false;

	// Activated buttons
	private GameObject _activatedButtons		= null;

	// Interact item result check
	private bool _interacting					= false;
	private C_InteractableObject _interactObj	= null;
	private int _interactConResIndex			= 0;

	#endregion

	void Start()
	{
		_offsetPos = player.transform.position - player.feet.bounds.center;
		foreach(C_Exit exit in _exits)
		{
			if(exit.nextScene == C_ChapterManager.currentChapter.lastScene)
			{
				//Vector3 newPos = exit.exitCollider.bounds.center;
				//newPos.y = exit.exitCollider.bounds.min.y;
				Vector3 newPos = exit.GetTargetPosition();

				player.transform.position = newPos + _offsetPos;
				player.transform.SetLocalPositionZ(0);
				if(exit.enterDirection == E_HorizontalDirection.Left)
				{
					//player.transform.SetLocalScaleXNegative();
					player.ChangeDirection(E_HorizontalDirection.Left);
				}

				break;
			}
		}

		foreach(string objectToDestroy in C_ChapterManager.currentChapter.destroyedObjects)
		{
			foreach(C_InWorldItem _item in _items)
			{
				//if(_item.item.itemName == objectToDestroy)
				if(_item.name == objectToDestroy)
				{
					if( _item.destroyableObject)
						Destroy(_item.gameObject);
					else
						_item.accessiblePlayer = E_Player.None;
				}
			}

			foreach(C_InteractableObject _object in _objects)
			{
				if(_object.name == objectToDestroy)
				{
					Destroy(_object.gameObject);
				}
			}
		}

	}

	void Awake()
	{
		if (theInputs == null)
			theInputs = C_Input.getInstance;
		if (characters != null)
			_characters = characters.GetComponentsInChildren<C_Character> (true);
		else
			Debug.LogWarning ("Characters are missing in SceneManager");
		if (exits != null)
			_exits = exits.GetComponentsInChildren<C_Exit> (true);
		else
			Debug.LogWarning ("Exits are missing in SceneManager");
		if (items != null)
			_items = items.GetComponentsInChildren<C_InWorldItem> (true);
		else
			Debug.LogWarning ("Items are missing in SceneManager");
		if (objects != null)
			_objects = objects.GetComponentsInChildren<C_InteractableObject> (true);
			//_objects = new List<C_InteractableObject>(objects.GetComponentsInChildren<C_InteractableObject> ());
		else
			Debug.LogWarning ("InteractableObjects are missing in SceneManager");
		if (descriptionBox == null)
			Debug.LogWarning ("DescriptionBox is missing in SceneManager");
		if (dialogueUI == null)
			Debug.LogWarning ("DialogueUI is missing in SceneManager");

		movementTimer.pauseStopwatch = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (_pause)
			return;

		if (_skipUpdate) {
			_skipUpdate = false;
			return;
		}

		if(_interacting)
		{
			// pause update when interacting
			return;
		}

		movementTimer.UpdateTimer ();

		theInputs.InputUpdate ();

		if(theInputs.I_Up)
		{
			if(_dialogueEnabled)
			{
				if(!_dialogueQuestion)
				{
					// Next dialogue
					if(!C_DialogueManager.getInstance.MoveToNextNode())
						_dialogueEnabled = false;

					// Make text setting into a function
					DialogueNodeResult();
				}

				_dialogueQuestion = dialogueUI.isQuestion;
			}
			else
			{
				Vector3 checkPos = Camera.main.ScreenToWorldPoint (theInputs.I_Up_Position);
				checkPos.z = 0;
				
				RaycastHit2D[] hitAll = Physics2D.RaycastAll(checkPos, Vector2.zero);
				// Check if player click on UI
				CheckAllMovement(hitAll,checkPos);

				if(_showButtons)
				{
					if(_activatedButtons)
						_activatedButtons.SetActive(false);
					_activatedButtons = null;
					_showButtons = false;
				}
				
				dialogueUI.gameObject.SetActive (false);
			}
			
			if(_clickButton)
				_clickButton = false;
			else if(descriptionBox.activeSelf)
				descriptionBox.SetActive (false);
		}

		if (_movePlayer)
			MovePlayer (_targetPos);
		else
			player.isMoving = false;

		if(_dialogueEnabled)
			dialogueUI.gameObject.SetActive (true);
		else
			dialogueUI.gameObject.SetActive (false);
	}

	#region Button OnClick functions
	
	public void SlotClicked(int index)
	{
		_objectClickedID = -1;
		C_InventoryManager theInventory = player.inventory;

		// if clicked on same slot again
		if (index == theInventory.selectedSlot) {
			theInventory.selectedSlot = -1;
			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			_clickButton = true;
			_movePlayer = false;
			return;
		}

		// if clicked on another item
		if (theInventory.inventorySlots [index].itemInSlot != null) {
			if (theInventory.selectedSlot == -1) {
				theInventory.selectedSlot = index;
				_selectedItem = "selected_" + theInventory.inventorySlots [theInventory.selectedSlot].itemInSlot.itemName;
				C_ChapterManager.currentChapter.conditionTriggers.Add(_selectedItem);
				_clickButton = true;
				_movePlayer = false;
				return;
			} else {
				// Check if can combine
				theInventory.CombineSlot(theInventory.selectedSlot,index);
				
				theInventory.selectedSlot = -1;
				C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
				_selectedItem = "";
			}
		}
		// if clicked on empty slot
		else {
			theInventory.selectedSlot = -1;
			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			Debug.Log ("Itemslot " + index + " is empty");
		}

		_clickButton = true;
		_movePlayer = false;
	}

	public void ShowDescription(C_InWorldItem theItem)
	{
		theItem.buttonsCurrent.SetActive (false);
		if(PlayerPrefs.GetInt("Language") == 1)
			DisplayDescriptionBox(theItem.item.itemDescription);
		else
			DisplayDescriptionBox(theItem.item.itemDescriptionDutch);
	}
	
	public void ObserveObject(C_InteractableObject theObject)
	{
		theObject.buttons.SetActive (false);
		//DisplayDescriptionBox(theObject.observeLine);
		if(PlayerPrefs.GetInt("Language") == 1)
			DisplayDescriptionBox(theObject.observeLine);
		else
			DisplayDescriptionBox(theObject.observeLineDutch);
	}

	public void InteractObject(C_InteractableObject theObject)
	{
		theObject.buttons.SetActive (false);
		_skipUpdate = true;

		switch(theObject.interactionType)
		{
		case E_InteractType.None:

			break;

		case E_InteractType.Monologue:

			SetupDialogue(true);
			_dialogueEnabled = true;

			break;

		case E_InteractType.LoadScene:
			
			C_ChapterManager.currentChapter.lastScene = Application.loadedLevelName;
			Application.LoadLevel(theObject.loadScene);
			break;

		}
	}

	public void DisplayDescriptionBox(string line)
	{
		_activatedButtons = null;
		_showButtons = false;
		descriptionBox.SetActive (true);
		Text description = descriptionBox.GetComponentInChildren<Text> ();
		description.text = line;
		_clickButton = true;
	}
	
	public void PickUpItem(C_InWorldItem theItem)
	{
		if (!ObtainItem (theItem.item))
		{
			_clickButton = true;
			return;
		}

		// Set conditions
		//C_ChapterManager.currentChapter.pickedUpItems.Add (theItem.item.itemName);
		C_ChapterManager.currentChapter.destroyedObjects.Add (theItem.name);

		_showButtons = false;
		_activatedButtons = null;
		Destroy (theItem.buttonsCurrent);
		if (theItem.destroyableObject)
			Destroy (theItem.gameObject);
		else
			theItem.accessiblePlayer = E_Player.None;
		_clickButton = true;
	}

	public void DialogueOption(int option)
	{
		dialogueUI.ChangeDialogueBox (0);
		C_DialogueNode parent = C_DialogueManager.getInstance.currentNode.parentNode;
		C_DialogueManager.getInstance.currentNode = parent.listOfChildNodes [option].listOfChildNodes[0];
		DialogueNodeResult ();
	}
	
	public void PauseButton()
	{
		PauseMenu(true);
		Time.timeScale = 0;
		_pause = true;
	}

	public void ResumeButton()
	{
		PauseMenu(false);
		_skipUpdate = true;
		Time.timeScale = 1;
		_pause = false;
	}
	
	public void MainMenuButton()
	{
		Time.timeScale = 1;
		// MAY need to destroy chapter manager instance to reset chapter progression
		C_ChapterManager.currentChapter = null;
		Application.LoadLevel ("MainMenu");
	}

	#endregion

	#region Input Collision Checks

	void CheckAllMovement(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		if(CheckIfUI(hitAll, checkPos))
		{
			// May not need CheckIfUI if use Unity's Button feature instead
		}
		// Check if player click on Interactable Object
		// Check object collision before npc and items because it may be meant to block out some stuff
		else if(CheckIfObject(hitAll, checkPos) && !_clickButton)
		{
			//C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			//_selectedItem = "";
			//player.inventory.selectedSlot = -1;
			_timeToMove = (_targetPos - _startPos).magnitude / player.speed;
			_movePlayer = true;
			// reset exit scene to empty
			_theExitScene = "";
			// start movement timer
			movementTimer.Reset();
			movementTimer.pauseStopwatch = false;
		}
		// Check if player click on NPC
		else if(CheckIfNPC(hitAll, checkPos) && !_clickButton)
		{
			player.inventory.selectedSlot = -1;
			_timeToMove = (_targetPos - _startPos).magnitude / player.speed;
			_movePlayer = true;
			_objectClickedID = -1;
			// reset exit scene to empty
			_theExitScene = "";
			// start movement timer
			movementTimer.Reset();
			movementTimer.pauseStopwatch = false;
		}
		// Check if player click on In World Item
		else if(CheckIfItem(hitAll, checkPos) && !_clickButton)
		{
			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			player.inventory.selectedSlot = -1;
			_timeToMove = (_targetPos - _startPos).magnitude / player.speed;
			_movePlayer = true;
			_objectClickedID = -1;
			// reset exit scene to empty
			_theExitScene = "";
			// start movement timer
			movementTimer.Reset();
			movementTimer.pauseStopwatch = false;
		}
		// Check if player click on a scene exit
		else if(CheckIfExit(hitAll, checkPos) && !_clickButton)
		{
			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			player.inventory.selectedSlot = -1;
			_timeToMove = (_targetPos - _startPos).magnitude / player.speed;
			_movePlayer = true;
			// reset itemClickedID
			_itemClickedID = -1;
			_objectClickedID = -1;
			// start movement timer
			movementTimer.Reset();
			movementTimer.pauseStopwatch = false;
		}
		// Check if player click on the ground
		else if(CheckMovement(hitAll, checkPos) && !_clickButton)
		{
			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			player.inventory.selectedSlot = -1;
			_timeToMove = (_targetPos - _startPos).magnitude / player.speed;
			_movePlayer = true;
			// reset itemClickedID
			_itemClickedID = -1;
			_objectClickedID = -1;
			// reset exit scene to empty
			_theExitScene = "";
			// start movement timer
			movementTimer.Reset();
			movementTimer.pauseStopwatch = false;
		}
	}

	// If clicked on buttons etc.
	bool CheckIfUI(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		return false;
	}
	
	// If clicked on Interactable Object
	bool CheckIfObject(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		Vector3 checkPos1 = player.transform.position - _offsetPos;
		checkPos1.z = 0;
		
		if ((checkPos - checkPos1).sqrMagnitude > player.minimumDistance)
		{
			int i = 0;
			foreach (C_InteractableObject child in _objects) {
				foreach (RaycastHit2D hit in hitAll) {
					if (hit.collider == child.clickCollider) {
						
						Vector3 tempPos = child.interactCollider.bounds.center;
						tempPos.z = 0;
						
						_targetPos = tempPos;
						//_targetPos = checkPos;
						_startPos = player.feet.bounds.center;
						_startPos.z = 0;
						_objectClickedID = i;
						return true;
					}
				}
				i++;
			}
		}
		
		return false;
	}

	// If clicked on inventory slots, MAY NOT NEED if use Unity's Buttons
	bool CheckIfNPC(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		Vector3 checkPos1 = player.transform.position - _offsetPos;
		checkPos1.z = 0;
		
		if ((checkPos - checkPos1).sqrMagnitude > player.minimumDistance)
		{
			int i = 0;
			foreach (C_Character child in _characters) {
				foreach (RaycastHit2D hit in hitAll) {
					if (hit.collider == child.clickCollider) {
						
						Vector3 tempPos = child.interactCollider.bounds.center;
						tempPos.z = 0;
						
						_targetPos = tempPos;
						//_targetPos = checkPos;
						_startPos = player.feet.bounds.center;
						_startPos.z = 0;
						_npcClickedID = i;
						return true;
					}
				}
				i++;
			}
		}

		return false;
	}

	// If clicked on In World Item
	bool CheckIfItem(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		Vector3 checkPos1 = player.transform.position - _offsetPos;
		checkPos1.z = 0;
		
		if ((checkPos - checkPos1).sqrMagnitude > player.minimumDistance)
		{
			int i = 0;
			foreach (C_InWorldItem child in _items) {
				foreach (RaycastHit2D hit in hitAll) {
					if (hit.collider == child.clickCollider) {

						Vector3 tempPos = child.interactCollider.bounds.center;
						tempPos.z = 0;

						_targetPos = tempPos;
						//_targetPos = checkPos;
						_startPos = player.feet.bounds.center;
						_startPos.z = 0;
						_itemClickedID = i;
						return true;
					}
				}
				i++;
			}
		}

		return false;
	}

	// If clicked on Exits
	bool CheckIfExit(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
		foreach(C_Exit child in _exits)
		{
			foreach(RaycastHit2D hit in hitAll)
			{
				if (hit.collider == child.exitCollider)
				{
					//checkPos = child.exitCollider.bounds.center;
					//checkPos.y = child.exitCollider.bounds.min.y;
					_targetPos = child.GetTargetPosition();
					_startPos = player.feet.bounds.center;
					_startPos.z = 0;
					Debug.DrawRay(_startPos,_targetPos - _startPos, Color.white,2);
					_theExitScene = child.nextScene;
					return true;
				}
			}
		}

		return false;
	}

	// If clicked on ground
	bool CheckMovement(RaycastHit2D[] hitAll, Vector3 checkPos)
	{
//		foreach(RaycastHit2D hit in hitAll)
//		{
//			if (hit.collider == background)
//			{
//				_targetPos = checkPos;
//				_startPos = player.feet.bounds.center;
//				return true;
//			}
//		}

		Vector3 checkPos1 = player.transform.position - _offsetPos;
		checkPos1.z = 0;
		
		if((checkPos-checkPos1).sqrMagnitude > player.minimumDistance)
		{
			_targetPos = checkPos;
			_startPos = player.feet.bounds.center;
			_startPos.z = 0;

			return true;
		}
		return false;
	}

	#endregion

	#region Movement Functions

	void MovePlayer(Vector3 targetPos)
	{
		// Problem with movement timer
		float percentage = movementTimer.inSeconds () / _timeToMove;

		if (percentage < 1.0f)
		{
			player.isMoving = true;
			Vector3 movementVector = MovementVector (_startPos, targetPos, percentage);
			
			int layermask = 1 << LayerMask.NameToLayer("Ground");
			Vector3 theStart = player.transform.position - _offsetPos;
			Vector3 theEnd = _startPos + movementVector;
			theStart.z = -10;
			theEnd.z = -10;

			// Check RayCast here if hit unwalkable region
			// before moving player
			RaycastHit2D hit = Physics2D.Raycast(theEnd, theStart-theEnd, (theStart-theEnd).magnitude, layermask);
			if (hit.collider == walkableRegion && hit.fraction != 0)
			{
				Vector3 movementCheck = theEnd-theStart;
				Vector3 edgeVector = new Vector3();
				edgeVector.x = -hit.normal.y;
				edgeVector.y = hit.normal.x;

				// Change direction of movement
				// However, this constantly updates, causing the player to always loop movement
				if(movementCheck.x > 0 && edgeVector.x < 0)
					edgeVector = -edgeVector;
				else if(movementCheck.x < 0 && edgeVector.x > 0)
					edgeVector = -edgeVector;

				edgeVector *= player.speed * Time.deltaTime;

				Debug.DrawLine(theStart,(((Vector3)edgeVector)+theStart),Color.red,2,true);

				RaycastHit2D hit2 = Physics2D.Raycast(theStart+edgeVector, -edgeVector, (edgeVector).magnitude, layermask);
				if (hit2.collider != walkableRegion || hit2.fraction == 0)
				{
					player.transform.position = player.transform.position + edgeVector;
					player.transform.SetLocalPositionZ(0.0f);
					if((player.transform.position.x-_targetPos.x) > player.minimumDistance || (player.transform.position.x-_targetPos.x) < -player.minimumDistance)
					{
						RaycastHit2D[] hitAll = Physics2D.RaycastAll(_targetPos, Vector2.zero);
						CheckAllMovement(hitAll,_targetPos);
					}
					else
					{
						StopPlayerMovement();
					}
				}
				else
					StopPlayerMovement();

				// stop player movements
				//StopPlayerMovement();
			}
			// if clicked on (NPC, items, etc) and need collision check
			else if(_npcClickedID >= 0 && player.feet.bounds.Intersects(_characters[_npcClickedID].interactCollider.bounds))
			{
				// stop player movements
				StopPlayerMovement();

				// Check if inventory slot is clicked,
				// then check interactions

				// set and show dialogue
				SetupDialogue();

				_dialogueEnabled = true;
			}
			else if(_itemClickedID >= 0 && player.feet.bounds.Intersects(_items[_itemClickedID].interactCollider.bounds))
			{
				// stop player movements
				StopPlayerMovement();
				_activatedButtons = _items[_itemClickedID].buttonsCurrent;
				_activatedButtons.ToggleActive();
				_itemClickedID = -1;
				_showButtons = true;
			}
			else if(_objectClickedID >= 0 && player.feet.bounds.Intersects(_objects[_objectClickedID].interactCollider.bounds))
			{
				// stop player movements
				StopPlayerMovement();

				InteractWithObject(_objects[_objectClickedID]);
				//_objects[_objectClickedID].buttons.ToggleActive();
				_objectClickedID = -1;
				//_showButtons = true;
			}
			else
			{
				player.transform.position = _startPos + _offsetPos + movementVector;
				player.transform.SetLocalPositionZ(0.0f);
			}

			if(movementVector.x < 0 && player.currDirection == E_HorizontalDirection.Right)
			{
				player.ChangeDirection(E_HorizontalDirection.Left);
			}
			else if(movementVector.x > 0 && player.currDirection == E_HorizontalDirection.Left)
			{
				player.ChangeDirection(E_HorizontalDirection.Right);
			}
		}
		else
		{
			StopPlayerMovement();
			if(_theExitScene != "")
			{
				C_ChapterManager.currentChapter.lastScene = Application.loadedLevelName;
				Application.LoadLevel(_theExitScene);
			}
//			if(_itemClickedID >= 0)
//			{
//				_activatedButtons = _items[_itemClickedID].buttonsCurrent;
//				_activatedButtons.ToggleActive();
//				_itemClickedID = -1;
//				_showButtons = true;
//			}
		}
	}

	Vector3 MovementVector(Vector3 start, Vector3 end, float percentage)
	{
		percentage = Mathf.Clamp01 (percentage);

		Vector3 startToEnd = end - start;

		return percentage * startToEnd;
	}

	void StopPlayerMovement()
	{
		movementTimer.pauseStopwatch = true;
		movementTimer.Reset();
		player.isMoving = false;
		_movePlayer = false;
	}

	#endregion

	void PauseMenu(bool activate = true)
	{
		if (activate)
		{
			pauseMenu.SetActive(true);
		}
		else
		{
			pauseMenu.SetActive(false);
		}
	}

	void InteractWithObject(C_InteractableObject theObject)
	{
		bool noResult = true;
		if (theObject.conditionResultList.Count != 0)
		{
			foreach(C_ConditionResult conditionResult in theObject.conditionResultList)
			{
				bool checkFailed = false;

				foreach(string condition in conditionResult.conditionsList)
				{
					string trigger = condition;
					if(condition[0] == '!')
					{
						trigger = condition.Remove(0,1);
						
						if(trigger.StartsWith("have_"))
						{
							trigger = trigger.Remove(0,5);
							if(player.inventory.GetItemID(trigger) >= 0)
							{
								checkFailed = true;
								break;
							}
						}

						if(C_ChapterManager.currentChapter.conditionTriggers.Contains(trigger))
						{
							checkFailed = true;
							break;
						}
					}
					else
					{
						trigger = condition;
						if(trigger.StartsWith("have_"))
						{
							trigger = trigger.Remove(0,5);
							if(player.inventory.GetItemID(trigger) < 0)
							{
								checkFailed = true;
								break;
							}
						}

						if(!C_ChapterManager.currentChapter.conditionTriggers.Contains(condition))
						{
							checkFailed = true;
							break;
						}
					}
				}
				
				if(!checkFailed)
				{
					// Do result
					noResult = false;
					_interactObj = theObject;
					_interactConResIndex = theObject.conditionResultList.IndexOf(conditionResult);
					_interacting = true;
					InteractionResult();
					break;
				}
			}
		}

		if(noResult)
		{
			_activatedButtons = theObject.buttons;
			_activatedButtons.SetActive(true);
			_showButtons = true;
		}
		
		C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
		_selectedItem = "";
		player.inventory.selectedSlot = -1;
	}

	void InteractionResult()
	{
		List<C_Result> t_resultsList = _interactObj.conditionResultList [_interactConResIndex].resultsList;
		foreach(C_Result result in t_resultsList)
		{
			// is last result
			if(t_resultsList.IndexOf(result) == t_resultsList.Count-1)
				StartCoroutine(DoingResult(result,true));
			else
				StartCoroutine(DoingResult(result));
		}

	}

	IEnumerator DoingResult(C_Result result, bool last = false)
	{
		// Wait for buffer time
		if(result.haveBuffer)
			yield return new WaitForSeconds (result.bufferTime);

		// Do Results
		//Debug.Log (result.resultType.ToString());

		switch(result.resultType)
		{
		case E_ResultType.ShowLine:

			DisplayDescriptionBox(result.theSentence);

			break;
			
		case E_ResultType.ChangeAnimation:

			_interactObj.GetComponent<Animator>().SetBool(result.animationBool,true);

			break;
			
		case E_ResultType.GiveItem:

			ObtainItem(result.obtainItem);

			break;
			
		case E_ResultType.DeleteObject:

			C_ChapterManager.currentChapter.destroyedObjects.Add (result.destroyGameObject.name);
			Destroy(result.destroyGameObject);

			break;

		case E_ResultType.SetObjectActive:

			result.setGameObjectActive.SetActive(true);

			break;
			
		case E_ResultType.DeleteItem:
			
			player.inventory.inventorySlots[player.inventory.GetItemID(result.deleteItem)].DeleteItem();
			
			break;
			
		case E_ResultType.AddRemoveCondition:
			
			if(result.condition[0] == '!')
			{
				string trigger = result.condition.Remove(0,1);
				C_ChapterManager.currentChapter.conditionTriggers.Remove(trigger);
			}
			else
			{
				if(!C_ChapterManager.currentChapter.conditionTriggers.Contains(result.condition))
					C_ChapterManager.currentChapter.conditionTriggers.Add(result.condition);
			}
			
			break;

		default:
			break;
		}

		if(last)
			_interacting = false;
	}

	public bool ObtainItem(C_Item item)
	{
		// if inventory full
		if (C_Player.getInstance.inventory.GetEmptySlot () == null)
		{
			return false;
		}
		
		C_Player.getInstance.inventory.GetEmptySlot ().SetItem (item);
		C_ChapterManager.currentChapter.conditionTriggers.Add ("have_" + item.itemName);
		return true;
	}

	void SetupDialogue(bool monologue = false)
	{
		if (!monologue)
		{
			dialogueUI.characterNameLeft = player.dialogueGraphic.characterName;
			dialogueUI.characterNameRight = _characters [_npcClickedID].dialogueGraphic.characterName;
			dialogueUI.characterImageLeft.sprite = player.dialogueGraphic.characterSprite;
			dialogueUI.characterAnimatorLeft.runtimeAnimatorController = player.dialogueGraphic.characterAnimator;
			dialogueUI.characterImageRight.sprite = _characters [_npcClickedID].dialogueGraphic.characterSprite;
			dialogueUI.characterAnimatorRight.runtimeAnimatorController = _characters [_npcClickedID].dialogueGraphic.characterAnimator;
			dialogueUI.talkingCharacter = E_HorizontalDirection.Right;
			dialogueUI.ChangeDialogueBox (0);
			
			// Sets the node and checks for conditions, questions and options, etc
			C_DialogueManager.getInstance.SetCurrentNode (_characters [_npcClickedID].dialogueGraphic.characterName);
			DialogueNodeResult ();
		}
		else
		{
			dialogueUI.characterNameLeft = player.dialogueGraphic.characterName;
			dialogueUI.characterNameRight = "";
			dialogueUI.characterImageLeft.sprite = player.dialogueGraphic.characterSprite;
			dialogueUI.characterAnimatorLeft.runtimeAnimatorController = player.dialogueGraphic.characterAnimator;
			dialogueUI.characterImageRight.sprite = C_Constants.blank;
			dialogueUI.characterAnimatorRight.runtimeAnimatorController = null;
			dialogueUI.talkingCharacter = E_HorizontalDirection.Left;
			dialogueUI.ChangeDialogueBox (0);
			
			// Sets the node and checks for conditions, questions and options, etc
			C_DialogueManager.getInstance.SetCurrentNode ("Monologue");
			DialogueNodeResult ();
		}
	}

	void DialogueNodeResult()
	{
		C_DialogueNode theNode = C_DialogueManager.getInstance.currentNode;
		string trigger = "";

		switch(theNode.dialogueType)
		{
		case E_DialogueType.Line:

			dialogueUI.currentDialogueBox.choices[0].text = theNode.theText;
			break;
			
		case E_DialogueType.Condition:

			bool checkFailed = false;
			foreach(string condition in theNode.conditionsList)
			{
				if(condition[0] == '!')
				{
					trigger = condition.Remove(0,1);

					if(trigger.StartsWith("have_"))
					{
						trigger = trigger.Remove(0,5);
						if(player.inventory.GetItemID(trigger) >= 0)
						{
							checkFailed = true;
							break;
						}
					}

					if(C_ChapterManager.currentChapter.conditionTriggers.Contains(trigger))
					{
						checkFailed = true;
						break;
					}
				}
				else
				{
					trigger = condition;
					if(trigger.StartsWith("have_"))
					{
						trigger = trigger.Remove(0,5);
						if(player.inventory.GetItemID(trigger) < 0)
						{
							checkFailed = true;
							break;
						}
					}

					if(!C_ChapterManager.currentChapter.conditionTriggers.Contains(condition))
					{
						checkFailed = true;
						break;
					}
				}
			}

			if(checkFailed)
			{
				C_DialogueManager.getInstance.currentNode = C_DialogueManager.getInstance.GetNextSiblingNode(theNode);
				DialogueNodeResult();
			}
			else
			{
				if(!C_DialogueManager.getInstance.MoveToNextNode())
					_dialogueEnabled = false;
				
				DialogueNodeResult();
			}

			break;
			
		case E_DialogueType.Question:

			dialogueUI.talkingCharacter = E_HorizontalDirection.Right;
			dialogueUI.currentDialogueBox.choices[0].text = theNode.sentence;
			break;
			
		case E_DialogueType.Option:

			// NEED CHANGE DIALOGUE BOX HERE
			dialogueUI.ChangeDialogueBox(theNode.parentNode.choices-1);

			int index = 0;
			foreach(C_DialogueNode options in theNode.parentNode.listOfChildNodes)
			{
				dialogueUI.currentDialogueBox.choices[index].text = options.sentence;
				index++;
			}

			dialogueUI.talkingCharacter = E_HorizontalDirection.Left;

			break;
			
		case E_DialogueType.Obtain:

			foreach(C_Item item in _characters[_npcClickedID].npcInventory)
			{
				if(item.itemName == theNode.theText)
				{
					ObtainItem(item);
					break;
				}
			}

			// Next dialogue
			if(!C_DialogueManager.getInstance.MoveToNextNode())
				_dialogueEnabled = false;

			DialogueNodeResult();
			break;
			
		case E_DialogueType.BackToQuestion:

			theNode =	C_DialogueManager.getInstance.GetQuestionNode(theNode,theNode.question);

			if(theNode == null)
			{
				Debug.LogWarning("Unable to find question node for DialogueNode.\nQuestion: " + theNode.question);
			}
			else
			{
				C_DialogueManager.getInstance.currentNode = theNode;

				DialogueNodeResult();
			}
			break;
			
		case E_DialogueType.ContinueDialogue:

			theNode =	C_DialogueManager.getInstance.GetQuestionNode(theNode,theNode.question);

			theNode = C_DialogueManager.getInstance.GetNextSiblingNode(theNode);

			if(theNode == null)
			{
				Debug.LogWarning("Unable to find question node for DialogueNode.\nQuestion: " + theNode.question);
			}
			else
			{
				C_DialogueManager.getInstance.currentNode = theNode;
				
				DialogueNodeResult();
			}
			break;
			
		case E_DialogueType.LeaveDialogue:

			C_ChapterManager.currentChapter.conditionTriggers.Remove(_selectedItem);
			_selectedItem = "";
			_npcClickedID = -1;
			_dialogueEnabled = false;
			break;
			
		case E_DialogueType.ChangeCharacter:
			
			if(theNode.theText == "Left")
				dialogueUI.talkingCharacter = E_HorizontalDirection.Left;
			else if(theNode.theText == "Right")
				dialogueUI.talkingCharacter = E_HorizontalDirection.Right;
			else
				dialogueUI.talkingCharacter = E_HorizontalDirection.None;
			
			// Next dialogue
			if(!C_DialogueManager.getInstance.MoveToNextNode())
				_dialogueEnabled = false;

			DialogueNodeResult();
			break;
			
		case E_DialogueType.Result:

			switch(theNode.name)
			{
			case C_Constants.D_V_SETACTIVE:
				
				if(theNode.theText == "true")
				{
					foreach(GameObject go in inactiveGameobjects)
					{
						if(go.name == theNode.sentence)
						{
							go.SetActive(true);
						}
					}
				}
				else if(theNode.theText == "false")
					GameObject.Find(theNode.sentence).SetActive(false);
				break;
				
			case C_Constants.D_V_CONDITION:
				
				// Add condition
				if(theNode.theText[0] == '!')
				{
					trigger = theNode.theText.Remove(0,1);
					C_ChapterManager.currentChapter.conditionTriggers.Remove(trigger);
				}
				else
				{
					if(!C_ChapterManager.currentChapter.conditionTriggers.Contains(theNode.theText))
						C_ChapterManager.currentChapter.conditionTriggers.Add(theNode.theText);
				}
				break;

			case C_Constants.D_V_CHANGEPLAYER:
				
				foreach(E_Player playercheck in Enum.GetValues(typeof(E_Player)))
				{
					if(playercheck.ToString() == theNode.theText)
					{
						player.ChangePlayer(playercheck);
						break;
					}
				}
				break;
				
			case C_Constants.D_V_DELETEINVENTORY:

				int tempIndex = player.inventory.GetItemID(theNode.theText);
				if(tempIndex >= 0)
				{
					player.inventory.inventorySlots[tempIndex].DeleteItem();
				}
				
				break;
				
			case C_Constants.D_V_ADDINVENTORY:
				
				//ObtainItem();

				foreach(C_Character character in _characters)
				{
					if(character.name == dialogueUI.characterNameRight)
					{
						foreach(C_Item item in character.npcInventory)
						{
							if(item.name == theNode.theText)
							{
								ObtainItem(item);
								break;
							}
						}
						break;
					}
				}
				
				break;
				
			case C_Constants.D_V_LOADSCENE:
				
				Application.LoadLevel(theNode.theText);
				
				break;
				
			case C_Constants.D_V_ANIMATIONBOOL:

				if(theNode.theText == "true")
				{
					_characters[_npcClickedID].GetComponent<Animator>().SetBool(theNode.sentence,true);
				}
				else if(theNode.theText == "false")
					_characters[_npcClickedID].GetComponent<Animator>().SetBool(theNode.sentence,true);
				
				break;
				
			case C_Constants.D_V_ENDCHAPTER:
				
				C_ChapterManager.getInstance.ResetCurrentChapter();
				
				break;

			default:

				break;
			}

			// Next dialogue
			if(!C_DialogueManager.getInstance.MoveToNextNode())
				_dialogueEnabled = false;
			
			DialogueNodeResult();
			break;

		default:
			break;
		}
	}

}
