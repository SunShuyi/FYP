using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {
	private int levelWidth;
	private int levelHeight;
	
	public Transform backgroundTile;
	public Transform strawWallTile;
	public Transform Parent;
	
	private Color[] tileColours;
	
	public Color backgroundColour;
	public Color strawWallColour;
	
	public Texture2D levelTexture;

	void Start () {
		levelWidth = levelTexture.width;
		levelHeight = levelTexture.height;
		loadLevel ();
	}

	void Update () {
	}
	void loadLevel()
	{
		//set tile colours to be new color
		tileColours =  new Color[levelWidth + levelHeight];
		tileColours = levelTexture.GetPixels();
		
		for(int y = 0; y < levelHeight; y++)
		{
			for(int x = 0; x < levelWidth; x++)
			{
				if(tileColours[x+y*levelWidth] == backgroundColour)
				{
					GameObject theTile = Instantiate(backgroundTile, new Vector3(x, y), Quaternion.identity) as GameObject;
					//theTile.transform.parent = Parent;
				}
				if(tileColours[x+y*levelWidth] == strawWallColour)
				{
					GameObject theTile = Instantiate(strawWallTile, new Vector3(x, y, 1), Quaternion.identity) as GameObject;
					//theTile.transform.parent = Parent;
				}
			}
		}
	}
}