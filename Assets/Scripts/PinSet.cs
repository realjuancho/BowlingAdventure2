using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSet : MonoBehaviour {


	
	List<PinPosition> pinPositions = new List<PinPosition>();

	public bool BallInGoal;
	public int StandingPins;

	// Use this for initialization
	void Start () {

		
		Pin[] pins = GetComponentsInChildren<Pin>();

		foreach(Pin p in pins)
		{
			PinPosition pin = new PinPosition(p, p.transform.position, p.transform.rotation);

			pinPositions.Add(pin);
		}	
	}


	void Update()
	{
		int pinCount = 0;
		foreach(PinPosition pp in pinPositions)
		{
			
			if(pp.pin.pinState == Pin.PinState.Standing)
				pinCount++;

		}

		StandingPins = pinCount;
	}

	public void ResetPins()
	{
		foreach(PinPosition p in pinPositions)
		{
			p.pin.transform.position = p.position;
			p.pin.transform.rotation = p.rotation;

			p.pin.ResetPin();
		}

	}

	void OnTriggerEnter(Collider col)
	{
		BallControl ball = col.GetComponent<BallControl>();

		if(ball)
		{
			Debug.Log("BallHasEntered");
			BallInGoal = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		BallControl ball = col.GetComponent<BallControl>();

		if(ball)
		{
			Debug.Log("BallHasEntered");
			BallInGoal = false;
		}
	}

	public class PinPosition
	{	
		public Pin pin;
		public Vector3 position;
		public Quaternion rotation;

		public PinPosition(Pin pin, Vector3 position, Quaternion rotation)
		{
			this.pin = pin;
			this.position = position;
			this.rotation = rotation;
		}


	}
}
