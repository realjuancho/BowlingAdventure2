using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SelectionGroup))]
public class WheelSelector : MonoBehaviour {

	
	public float separationAngle = 30.0f;
    public Transform SelectionRayOrigin;
    public bool hasFocus;
    public bool selectItemOnRay;

	Quaternion toRotation;
	SelectablePage ParentPage;
	SelectableItem[] wheelItems;

	public TouchButtons moveLeft;
	public TouchButtons moveRight;

    bool sliding;

	enum SlideDirection { None, Left, Right }

	//Use this for initialization
	void Start () {

		ParentPage = GetComponentInParent<SelectablePage>();


		wheelItems = GetComponentsInChildren<SelectableItem>();

		if(wheelItems.Length > 0)
			foreach(SelectableItem i in wheelItems)
			{
				i.isEnabled = false;
			}

	}

	// Update is called once per frame
	void Update () {

		if(ParentPage)
		if(ParentPage.isSelected == SelectionManager.Selected.Yes)
		{
			ListenSlide();
		}
	}


	void FixedUpdate()
	{
		if(!sliding)
		{
			Debug.DrawRay(SelectionRayOrigin.position, Vector3.up, Color.red);
			
			Ray SelectionRay = new Ray(SelectionRayOrigin.position, Vector3.up);

			RaycastHit selectionHit;

			if(Physics.Raycast(SelectionRay, out selectionHit, 1.0f))
			{

				SelectableItem si = selectionHit.collider.gameObject.GetComponent<SelectableItem>();

				if(wheelItems.Length > 0)

				foreach(SelectableItem s in wheelItems)
				{
					if(s.Equals(si))
					{	
						s.isEnabled = true;
						if(selectItemOnRay) s.isSelected = SelectionManager.Selected.Yes;
					}
					else
					{
						s.isEnabled = false;
						if(!selectItemOnRay) s.isSelected = SelectionManager.Selected.No;
					}
				}
			}

		}
		
	}


	float duration = 1.0f;
	float counter = 0;
	void ListenSlide()
	{
		if(!sliding)
		{
			bool turningLeft = false;
			bool turningRight = false;

			if(!moveLeft)
			turningLeft = Input.GetKeyDown(KeyCode.LeftArrow) || TouchPadInput.GetButton("LeftArrow");
			if(!moveRight)
			turningRight = Input.GetKeyDown(KeyCode.RightArrow) || TouchPadInput.GetButton("RightArrow");;

			if(moveLeft) if(moveLeft.isTouched()) turningLeft = true;
			if(moveRight) if(moveRight.isTouched()) turningRight = true;

			if(turningLeft || turningRight )
			{

				toRotation = transform.rotation;

				if(turningLeft) 
					toRotation *= Quaternion.Euler(0f,0f,separationAngle); 
				else if(turningRight) 
					toRotation  *= Quaternion.Euler(0f,0f,-separationAngle); 

				sliding = true;
				counter = 0;
			}
		}

		else
		{
			
			SlideBalls(SlideDirection.Right);
		}

	}
	void SlideBalls(SlideDirection direction)
	{

		counter += Time.deltaTime;
		transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, counter / duration);
	
		if(counter > duration /2)
		{
			transform.rotation = toRotation;
			sliding = false;
		}
	}

}
