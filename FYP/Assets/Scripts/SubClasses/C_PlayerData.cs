using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_PlayerData : MonoBehaviour
{
	public E_Player player									= E_Player.None;
	public RuntimeAnimatorController animationController	= null;
	public Sprite playerSprite								= null;
	public C_DialogueGraphic dialogueGraphic				= null;

	//[HideInInspector]
	[ContextMenuItem("Reset", "ResetInventory")]
	public List<C_Item> inventory							= null;

	public void ResetInventory()
	{
		inventory = null;
	}

	public C_PlayerData(E_Player player, List<C_Item> inventory)
	{
		this.player = player;
		this.inventory = inventory;
	}

}
