using UnityEngine;
using System.Collections;

//	=======================
//		EXTENSION CLASS
//	=======================

public static class C_Extensions
{
	#region Transform Extensions

	public static void SetPositionX(this Transform t, float newX)
	{
		t.position = new Vector3 (newX, t.position.y, t.position.z);
	}
	
	public static void SetPositionY(this Transform t, float newY)
	{
		t.position = new Vector3 (t.position.x, newY, t.position.z);
	}
	
	public static void SetPositionZ(this Transform t, float newZ)
	{
		t.position = new Vector3 (t.position.x, t.position.y, newZ);
	}
	
	public static void SetLocalPositionX(this Transform t, float newX)
	{
		t.localPosition = new Vector3 (newX, t.localPosition.y, t.localPosition.z);
	}
	
	public static void SetLocalPositionY(this Transform t, float newY)
	{
		t.localPosition = new Vector3 (t.localPosition.x, newY, t.localPosition.z);
	}
	
	public static void SetLocalPositionZ(this Transform t, float newZ)
	{
		t.localPosition = new Vector3 (t.localPosition.x, t.localPosition.y, newZ);
	}
	
	public static void SetLocalScaleX(this Transform t, float newX)
	{
		t.localScale = new Vector3 (newX, t.localScale.y, t.localScale.z);
	}
	
	public static void SetLocalScaleY(this Transform t, float newY)
	{
		t.localScale = new Vector3 (t.localScale.x, newY, t.localScale.z);
	}
	
	public static void SetLocalScaleZ(this Transform t, float newZ)
	{
		t.localScale = new Vector3 (t.localScale.x, t.localScale.y, newZ);
	}
	
	public static void SetLocalScaleXNegative(this Transform t)
	{
		t.localScale = new Vector3 (-t.localScale.x, t.localScale.y, t.localScale.z);
	}
	
	public static void SetLocalScaleYNegative(this Transform t)
	{
		t.localScale = new Vector3 (t.localScale.x, -t.localScale.y, t.localScale.z);
	}
	
	public static void SetLocalScaleZNegative(this Transform t)
	{
		t.localScale = new Vector3 (t.localScale.x, t.localScale.y, -t.localScale.z);
	}

	#endregion
	
	public static void ToggleActive(this GameObject go)
	{
		go.SetActive (!go.activeSelf);
	}

}
