using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider3D : MonoBehaviour {


	public int MaxValue = 10;
	public int MinValue = 0;

	public int Value;


	public Transform handler;
	public Transform bar;
	Bounds boundsBar;


	void Start()
	{
		boundsBar = bar.gameObject.GetComponent<MeshRenderer>().bounds;
	}


	void CheckDistance()
	{
		Vector3 leftLimit = new Vector3(bar.position.x - boundsBar.extents.x, bar.position.y, bar.position.z);
		Vector3 rightLimit = new Vector3(bar.position.x + boundsBar.extents.x, bar.position.y, bar.position.z);

		int steps = MaxValue - MinValue;

		float distance = Vector3.Distance(leftLimit, rightLimit);

		float valuePerUnit = distance / steps;

		//Debug.Log(valuePerUnit);


		float distanceFromLeft = Vector3.Distance(handler.position, leftLimit);

	 	Value = Mathf.RoundToInt(Mathf.Clamp(distanceFromLeft / valuePerUnit, MinValue, MaxValue));

	}



	bool touchingHandler;
	float handlerSpeed = 1.0f;
	void Update()
	{
		if(TouchPadInput.GetTouchesCount() > 0)
		{
			GameObject go = TouchPadInput.TouchedObject(); 
			if(go == handler.gameObject)
			{
				touchingHandler = true;
			}
		}
		else
		{
			touchingHandler = false;
		}

		if(touchingHandler)
		{
			Vector3 touchPos = Camera.main.ScreenToViewportPoint(TouchPadInput.CurrentTouchPosition());
			Vector2 handlerPos1 = Camera.main.WorldToViewportPoint(handler.position); 

			float t = Time.deltaTime;

			float difference =  touchPos.x - handlerPos1.x;

			if(difference > 0)
			{
				
				Vector3 rightLimit = new Vector3(bar.position.x + boundsBar.extents.x, bar.position.y, bar.position.z);


				float rightX = (rightLimit.x - handler.position.x);

			
				if(rightX > 0)
					handler.transform.Translate(-t*handlerSpeed,0f,0f);

			}
			else if(difference < 0)
			{
				Vector3 leftLimit = new Vector3(bar.position.x - boundsBar.extents.x, bar.position.y, bar.position.z);
				float leftX = (handler.position.x - leftLimit.x);
				if(leftX > 0)
					handler.transform.Translate(t*handlerSpeed,0f,0f);

			}

		}

		//Keep handler z axis the same as the bar's;
		Vector3 handlerPos = handler.position;
		Vector3 barPos = bar.position;
		handlerPos.y = barPos.y;
		handlerPos.z = barPos.z;
		handler.position = handlerPos;

		CheckDistance();
	}
}
