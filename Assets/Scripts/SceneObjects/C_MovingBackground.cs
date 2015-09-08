using UnityEngine;
using System.Collections;

public class C_MovingBackground : MonoBehaviour
{
	public E_HorizontalDirection horizontalDirection	= E_HorizontalDirection.None;
	public E_VerticalDirection verticalDirection		= E_VerticalDirection.None;

	public float horizontalSpeed						= 0.0f;
	public float verticalSpeed							= 0.0f;

	public BoxCollider2D boundary						= new BoxCollider2D();

	public bool resetPos								= false;

	// Update is called once per frame
	void Update ()
	{
		resetPos = false;

		if(horizontalDirection == E_HorizontalDirection.Left)
		{
			if(gameObject.transform.position.x <= boundary.bounds.min.x)
			{
				gameObject.transform.SetPositionX(boundary.bounds.max.x);
				resetPos = true;
			}
			
			gameObject.transform.SetPositionX(gameObject.transform.position.x - Time.deltaTime*horizontalSpeed);
		}
		
		if(horizontalDirection == E_HorizontalDirection.Right)
		{
			if(gameObject.transform.position.x >= boundary.bounds.max.x)
			{
				gameObject.transform.SetPositionX(boundary.bounds.min.x);
				resetPos = true;
			}
			
			gameObject.transform.SetPositionX(gameObject.transform.position.x + Time.deltaTime*horizontalSpeed);
		}
		
		if(verticalDirection == E_VerticalDirection.Up)
		{
			if(gameObject.transform.position.y >= boundary.bounds.max.y)
			{
				gameObject.transform.SetPositionY(boundary.bounds.min.y);
				resetPos = true;
			}
			
			gameObject.transform.SetPositionY(gameObject.transform.position.y + Time.deltaTime*verticalSpeed);
		}
		
		if(verticalDirection == E_VerticalDirection.Down)
		{
			if(gameObject.transform.position.y <= boundary.bounds.min.y)
			{
				gameObject.transform.SetPositionY(boundary.bounds.max.y);
				resetPos = true;
			}
			
			gameObject.transform.SetPositionY(gameObject.transform.position.y - Time.deltaTime*verticalSpeed);
		}

	}
}
