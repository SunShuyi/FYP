using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_Player : MonoBehaviour
{
	#region Singleton Structure
	
	private static C_Player _instance;
	private C_Player(){}
	
	public static C_Player getInstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (C_Player)FindObjectOfType(typeof(C_Player));
				if(_instance == null)
					_instance = new C_Player();
				//DontDestroyOnLoad(_instance);
			}
			return _instance;
		}
	}
	
	#endregion

	[HideInInspector]
	public BoxCollider2D feet					= null;
	[HideInInspector]
	public Animator animator					= null;
	[HideInInspector]
	// May need to change SpriteRenderer to Image
	public SpriteRenderer spriteRenderer		= null;
	[HideInInspector]
	public C_Chapter chapterManager			= null;
	public C_InventoryManager inventory			= null;
	[HideInInspector]
	public C_DialogueGraphic dialogueGraphic	= null;

	public bool isMoving						= false;
	public E_HorizontalDirection currDirection	= E_HorizontalDirection.Right;
	public float speed							= 3.0f;
	public float minimumDistance				= 0.5f;
	public E_Player currentPlayer				= E_Player.None;

	// Use this for initialization before any Start functions
	void Awake ()
	{
		// Getting reference to ALL Linked GameObject Components
		if (gameObject.GetComponent<BoxCollider2D> ())
			feet = gameObject.GetComponent<BoxCollider2D> ();
		if (gameObject.GetComponent<Animator> ())
			animator = gameObject.GetComponent<Animator> ();
		if (gameObject.GetComponent<SpriteRenderer> ())
			spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (inventory == null)
			Debug.LogWarning ("InventoryManager is missing from Player");

		chapterManager = C_ChapterManager.currentChapter;

	}

	void Start()
	{
		if (chapterManager == null)
			chapterManager = C_ChapterManager.currentChapter;
		ChangePlayer (C_ChapterManager.currentChapter.currentPlayer);
	}

	void OnDestroy()
	{
		// Save the player's inventory
		if (Application.isLoadingLevel)
			SavePlayerData ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isMoving)
			animator.SetBool("isWalking",true);
		else
			animator.SetBool("isWalking",false);

		// Just to test character swapping
//		if(Input.GetMouseButtonDown(1))
//		{
//			if(currentPlayer == E_Player.Odysseus)
//				ChangePlayer(E_Player.Eurylochus);
//			else if(currentPlayer == E_Player.Eurylochus)
//				ChangePlayer(E_Player.Odysseus);
//		}

	}

	public void ChangeDirection(E_HorizontalDirection dir)
	{
		if (currDirection == dir)
		{
			return;
		}

		currDirection = dir;
		if (currDirection == E_HorizontalDirection.Right)
		{
			if(gameObject.transform.localScale.x < 0)
			{
				gameObject.transform.SetLocalScaleXNegative();
			}
		}
		else
		{
			if(gameObject.transform.localScale.x > 0)
			{
				gameObject.transform.SetLocalScaleXNegative();
			}
		}
	}

	#region Change Player functions

	public void ChangePlayer(E_Player player)
	{
		bool inventoryNull = false;

		chapterManager.AddPlayer (player, null);
		SavePlayerData ();

		C_PlayerData tempData = chapterManager.GetPlayer (player);
		animator.runtimeAnimatorController = tempData.animationController;
		spriteRenderer.sprite = tempData.playerSprite;
		dialogueGraphic = tempData.dialogueGraphic;
		currentPlayer = player;
		C_ChapterManager.currentChapter.currentPlayer = player;

		if (chapterManager.GetPlayerInventory (player) == null)
			inventoryNull = true;

		for (int i=0; i<C_InventoryManager.maxSlots; i++) {
			if(inventoryNull)
				inventory.inventorySlots [i].SetItem (null);
			else
				inventory.inventorySlots [i].SetItem ((chapterManager.GetPlayerInventory (player))[i]);
		}

	}

	void SavePlayerData()
	{
		if(currentPlayer != E_Player.None)
			chapterManager.SavePlayer (currentPlayer,inventory.GetInventory());
	}

	#endregion

}
