using UnityEngine;
using System.Collections;

public class TouchPadInput : MonoBehaviour {

	public GameObject originPoint;
	public GameObject referenceGameObject;
	public GameObject arrowPoint;


	public bool enableLeftStick = true;


	//public TouchButtons[] buttons;


	Camera cam;

	void Awake()
	{
		cam = Camera.main;
	}
		
	void Update()
	{

		//CheckForButtonPress();

		if(enableLeftStick)
		{
			CheckForMovementTouch();
		}
	}

	bool isMovementFingerOn;
	int lastTouchCount = 0;

	void CheckForMovementTouch()
	{
		#region Touch


		/*
	    if(Input.touches.Length > 0)
		{

			Touch touch = Input.GetTouch(0);
			Vector3 fingerPos = touch.position;


			Camera	cam = Camera.main;		
			//Posicionar el vector en el lugar donde el usuario tiene el dedo mas .5 unidades para ver el objeto

			fingerPos.z = cam.nearClipPlane + 0.5f;
		
            Vector3 objPos = cam.ScreenToWorldPoint (fingerPos);

			//Verificar si el usuario comenzó a presionar el dedo
			if(lastTouchCount == 0)
			{
				
				fingerPos.z = cam.nearClipPlane + 0.8f;
				Vector3 arrowPos = cam.ScreenToWorldPoint(fingerPos);
			
					
				arrowPoint.transform.position = arrowPos;
				arrowPoint.transform.LookAt(objPos);

				referenceGameObject.transform.position = arrowPos;
				referenceGameObject.transform.LookAt(objPos);

			}


			originPoint.transform.position = objPos;

			//TODO: Touchpad
			if(touch.position.x < Screen.width /2 && touch.position.x > 0 &&
				touch.position.y < Screen.height && touch.position.y > 0)

			{
				isMovementFingerOn = true;
				arrowPoint.transform.LookAt(originPoint.transform.position);

			}
			else
			{
				isMovementFingerOn = false;
			}
		}
		else
		{
			isMovementFingerOn = false;
			lastTouchCount = 0;
		}

		ActivateObjects(isMovementFingerOn);

		//TODO: Touchpad
		lastTouchCount = Input.touches.Length;


		if(originPoint)
		{
			if(originPoint.activeInHierarchy && arrowPoint.activeInHierarchy)
			{


				
				Vector3 originPosToCam = cam.WorldToScreenPoint(originPoint.transform.position);
				Vector3 arrowPosToCam = cam.WorldToScreenPoint(arrowPoint.transform.position);

				Vector3 _2dOffset = originPosToCam - arrowPosToCam;
				float _2dDistance = _2dOffset.magnitude;

				Vector3 _2dDirection = _2dOffset / _2dDistance;

				float x = _2dDirection.x;
				float y = _2dDirection.y;

				//float z = _2dDirection.z;
				//Debug.Log("X:" + x + "   Y: " + y +  "   Z: " + z);


				MovementAxis_Horizontal = x ;
				MovementAxis_Vertical = y ;

			}
			else
			{
				MovementAxis_Horizontal = 0.0f;
				MovementAxis_Vertical = 0.0f;
			}
		}

		*/
		#endregion



		//TODO: Mouse emulation of touch
		if(Input.GetMouseButton(0))
		{
			Vector3 touch = Input.mousePosition;
			Vector3 fingerPos = touch;

			Camera	cam = Camera.main;		
			//Posicionar el vector en el lugar donde el usuario tiene el dedo mas .5 unidades para ver el objeto

			fingerPos.z = cam.nearClipPlane + 0.5f;
		
            Vector3 objPos = cam.ScreenToWorldPoint (fingerPos);

			//Verificar si el usuario comenzó a presionar el dedo
			if(lastTouchCount == 0)
			{
				
				fingerPos.z = cam.nearClipPlane + 0.8f;
				Vector3 arrowPos = cam.ScreenToWorldPoint(fingerPos);
			
					
				arrowPoint.transform.position = arrowPos;
				arrowPoint.transform.LookAt(objPos);

				referenceGameObject.transform.position = arrowPos;
				referenceGameObject.transform.LookAt(objPos);

			}


			originPoint.transform.position = objPos;


			if(touch.x < Screen.width /2 && touch.x > 0 &&
				touch.y < Screen.height && touch.y > 0)
			{
				isMovementFingerOn = true;
				arrowPoint.transform.LookAt(originPoint.transform.position);

			}
			else
			{
				isMovementFingerOn = false;
			}
		}
		else
		{
			isMovementFingerOn = false;
			lastTouchCount = 0;
		}

		ActivateObjects(isMovementFingerOn);


		//TODO: Mouse
		lastTouchCount = Input.GetMouseButton(0) ? 1 : 0 ;

		if(originPoint)
		{
			if(originPoint.activeInHierarchy && arrowPoint.activeInHierarchy)
			{


				
				Vector3 originPosToCam = cam.WorldToScreenPoint(originPoint.transform.position);
				Vector3 arrowPosToCam = cam.WorldToScreenPoint(arrowPoint.transform.position);

				Vector3 _2dOffset = originPosToCam - arrowPosToCam;
				float _2dDistance = _2dOffset.magnitude;

				Vector3 _2dDirection = _2dOffset / _2dDistance;

				float x = _2dDirection.x;
				float y = _2dDirection.y;

				//float z = _2dDirection.z;
				//Debug.Log("X:" + x + "   Y: " + y +  "   Z: " + z);


				MovementAxis_Horizontal = x ;
				MovementAxis_Vertical = y ;

			}
			else
			{
				MovementAxis_Horizontal = 0.0f;
				MovementAxis_Vertical = 0.0f;
			}
		}

	
	}

	public static int GetTouchesCount()
	{

		//touchpad:
		if(Input.touches.Length > 0)
			return Input.touches.Length;

		//mouse:
		if(Input.GetMouseButton(0)) 
			return 1;
		
		return 0;
		
	}

	public static bool GetButton(string buttonName)
	{
		
		//TODO: Uncomment 4 lines below for Touchpad
//		if (Input.touches.Length > 0) {	
//
//			for(int i = 0; i < Input.touches.Length; i++)
//			{
//				Touch buttonTouch = Input.GetTouch (i);

				//TODO: Uncomment 2 lines for Mouse & Keyboard
				if(Input.GetMouseButton(0)){
					Vector3 buttonTouch = Input.mousePosition;

					Camera cam = Camera.main;

					//TODO: Uncomment line below for TouchPad
					//Ray rayButton = cam.ScreenPointToRay(buttonTouch.position);

					//TODO: Uncomment for Mouse
					Ray rayButton = cam.ScreenPointToRay(buttonTouch);

					RaycastHit buttonHit = new RaycastHit();
					bool buttonHitFound = Physics.Raycast(rayButton, out buttonHit);


					if(DebugTouchPad.DrawTouchRay) Debug.DrawRay(cam.transform.position, rayButton.direction, Color.yellow, 3.0f);

					if (buttonHitFound) {
					
						TouchButtons touchedButton = buttonHit.collider.gameObject.GetComponent<TouchButtons> ();

						if (touchedButton) {
							
							touchedButton.setTouched (true);

							if(touchedButton.Name == buttonName)
								return true;
						}	
					
					}
				}
		//}





		return false;
	}

	public static Vector3 CurrentTouchPosition()
	{
		//TODO: Mouse
		if(Input.GetMouseButton(0)) return Input.mousePosition;

		//TODO: Touch
		if(Input.touches.Length > 0) return Input.GetTouch(0).position;

		return Vector3.zero;
	}

	public static GameObject TouchedObject()
	{
		//if (Input.touches.Length > 0) {	

//			for(int i = 0; i < Input.touches.Length; i++)
//			{
//				Touch buttonTouch = Input.GetTouch (i);

				//TODO: Mouse
				if(Input.GetMouseButton(0))
				{
					Vector3 buttonTouch = Input.mousePosition;

					Camera cam = Camera.main;

					//TODO: TouchPad
					//Ray rayButton = cam.ScreenPointToRay(buttonTouch.position);

					//TODO:Mouse
					Ray objectTouchedRay = cam.ScreenPointToRay(buttonTouch);
					RaycastHit objectTouchedHit = new RaycastHit();

					bool objectWasHit = Physics.Raycast(objectTouchedRay, out objectTouchedHit);

					if(DebugTouchPad.DrawTouchRay) Debug.DrawRay(cam.transform.position, objectTouchedRay.direction, Color.yellow, 3.0f);

					if (objectWasHit) {

						return objectTouchedHit.collider.gameObject;
					}
				
				}
		//}

		return null;
	}



	void ActivateObjects(bool Activate)
	{

		if(Activate && !originPoint.activeInHierarchy && !arrowPoint.activeInHierarchy)
		{
			originPoint.SetActive(true);
			arrowPoint.SetActive(true);
		}
		else if(!Activate)
		{
			if(originPoint)
			{
				originPoint.SetActive(false);
			}
			if(arrowPoint)
			{
				arrowPoint.SetActive(false);
			}
		}


	}

	public static float MovementAxis_Vertical;
	public static float MovementAxis_Horizontal;

	[System.Serializable]
	public static class DebugTouchPad
	{
		public static bool DrawTouchRay;
	}

	public static float GetAxis(string AxisName)
	{
		if(AxisName == "Vertical")
		{
			return MovementAxis_Vertical;
		}
		else if(AxisName == "Horizontal")
		{
			return MovementAxis_Horizontal;
		}

		return 0;
	}

}
