using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public PinState pinState;
	public enum PinState { Standing, Defeated }
	public DebugPin debugPin;

	Rigidbody rb;
	float timeSincePinDefeated = 0.0f;
	float maxColliderTimeSinceDefeated = 2.0f;
	MeshCollider pincollider;
	Bounds pinBounds;


	void Start () {
		rb = GetComponent<Rigidbody>();
		pincollider = GetComponent<MeshCollider>();

		pinBounds = pincollider.bounds;
	}
	

	void LateUpdate () {

		if(pinState != PinState.Defeated)
		{
			

			Vector3 vectorCheck = transform.position + Vector3.up * pinBounds.extents.y * 2;

			if(debugPin.DrawPinStateRay) Debug.DrawRay(vectorCheck, Vector3.up, Color.green);

			Ray checkPinStateRay = new Ray(vectorCheck, Vector3.up * 2.0f);
			RaycastHit checkPinStateRayHit;


			if(Physics.Raycast(checkPinStateRay, out checkPinStateRayHit, 2.0f))
			{
				if(checkPinStateRayHit.collider.name == "PinCheck" && checkPinStateRayHit.transform.parent == gameObject.transform.parent)
				{
					pinState = PinState.Standing;

				}

				else
				{
					pinState = PinState.Defeated;
				}
			}
			else
			{
				pinState= PinState.Defeated;
			}
		}
		else
		{

			timeSincePinDefeated += Time.deltaTime;
			if(timeSincePinDefeated >= maxColliderTimeSinceDefeated)
			{
				//pincollider.enabled = false;
			}
		}

	}

	public void ResetPin()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;


		pinState = PinState.Standing;
		pincollider.enabled = true;
	}

	[System.Serializable]
	public class DebugPin 
	{
		public bool DrawPinStateRay = true;
	}

}
