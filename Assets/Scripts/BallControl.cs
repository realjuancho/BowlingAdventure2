using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	

	public BallSettings ballSettings = new BallSettings();
	public DebugBall debugBall = new DebugBall();

	[Range(-3.0f, 3.0f)]
	public float xPitch = 0.0f;
	public float xPitchSensitivity = 0.1f;
	public float MaxOutOfBoundsTime = 2.0f;
	public float MinTimeNeededToTouchBallAndSetRolling = 1.0f;
	public float BallTouchSensitivity = 0.1f;


    private Rigidbody rb;
    WaypointCollection wpc;
    Waypoint currentWaypoint;
    PinSet pinSet;
    StartPosition startPosition;
    float timeSinceOutOfBounds = 0.0f;
    float timeSinceTouchingBall = 0.0f;

    Bounds bounds;
    bool ballGrounded = false;

    public enum BallState { Idle, OutOfBounds, Rolling };
    public BallState ballState;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        wpc = GameObject.FindObjectOfType<WaypointCollection>();
        pinSet = GameObject.FindObjectOfType<PinSet>();
        startPosition = GameObject.FindObjectOfType<StartPosition>();

        bounds = GetComponent<SphereCollider>().bounds;
    }

    void LateUpdate ()
    {
    	
		SetBallRolling();

		DetectBounds();

		BallPitch();
		BallJump();

     }

    

    void BallPitch()
    {
    	if(ballState == BallState.Rolling)
    	{

    		float pitchInput = TouchPadInput.GetAxis("Horizontal");


    		if(pitchInput > 0)
    			xPitch = Mathf.MoveTowards(xPitch, 3.0f, xPitchSensitivity);
			else if(pitchInput < 0)
				xPitch = Mathf.MoveTowards(xPitch, -3.0f, xPitchSensitivity);

			else xPitch = Mathf.MoveTowards(xPitch, 0, xPitchSensitivity);
    	}


    }
    void BallJump()
    {

    	if(ballState == BallState.Rolling)
    	{
	    	bool inputJump = Input.GetKey(KeyCode.Space) || TouchPadInput.GetButton("Jump");
	    	
	    	if(ballGrounded && inputJump)
	    	{

				rb.AddForce(Vector3.up * ballSettings.jumpForce, ForceMode.VelocityChange);
	    		
	    	}

	    	Color groundedRayColor = Color.red;

	    	Ray groundedRay = new Ray(transform.position, Vector3.down * bounds.extents.y);
	    	RaycastHit groundedHit;

	    	float adjustedextents = bounds.extents.y + 0.01f;

	    	if(Physics.Raycast(groundedRay, out groundedHit, adjustedextents))
	    	{
	    		Lane lane = groundedHit.collider.gameObject.GetComponent<Lane>();

	    		if(lane)
	    		{
	    			groundedRayColor = Color.green;
	    			ballGrounded = true;
	    		}
	    	}
	    	else
	    	{
	    		ballGrounded = false;
	    	}

	    	Debug.DrawRay(transform.position, Vector3.down * adjustedextents, groundedRayColor);

    	}
    }

    void DetectBounds()
    {

    	if(ballState == BallState.Rolling)
    	{
	    	Color rayColor;

			Ray boundsRay = new Ray(transform.position, Vector3.down);
			RaycastHit boundsHit;


			if(Physics.Raycast(boundsRay, out boundsHit))
			{
				OutOfBounds oob = boundsHit.collider.gameObject.GetComponent<OutOfBounds>();
				if(oob)
				{
					rayColor = Color.red;
					timeSinceOutOfBounds += Time.deltaTime;
				}
				else
				{
					rayColor = Color.green;
					timeSinceOutOfBounds = 0;
				}

				if(debugBall.ShowBoundsNormalRay) Debug.DrawRay(transform.position, boundsHit.normal, rayColor);
			}

			if(timeSinceOutOfBounds > MaxOutOfBoundsTime)
				ballState = BallState.OutOfBounds;
		}

    }

    void SetBallRolling()
    {
    	if(ballState == BallState.Idle)
    	{

			GameObject b = TouchPadInput.TouchedObject();

			if(b)
			{
	    		if(b.name == "Ball")
	    		{
	    			timeSinceTouchingBall += Time.deltaTime;
	    		}
	    		else
	    		{
	    			timeSinceTouchingBall = 0.0f;
	    		}

	    		if(timeSinceTouchingBall > MinTimeNeededToTouchBallAndSetRolling)
	    		{
					ballState =  BallState.Rolling;
					timeSinceTouchingBall = 0.0f;
				}
			}
			else
			{
				timeSinceTouchingBall = 0.0f;
			}

    	}
    	else if(ballState == BallState.Rolling)
    	{

		if(!currentWaypoint) 
			currentWaypoint = wpc.FirstWaypoint();

		else
			currentWaypoint = wpc.NextWaypoint(currentWaypoint, transform.position);

		Vector3 adjustedDirection;
		if(!currentWaypoint.IsLast)
		{
	
			adjustedDirection = new Vector3(currentWaypoint.transform.position.x - xPitch,
				currentWaypoint.transform.position.y,
				currentWaypoint.transform.position.z);

    	}
    	else
    	{
			adjustedDirection = new Vector3(pinSet.transform.position.x - xPitch,
				pinSet.transform.position.y,
				pinSet.transform.position.z);

    	}

		Vector3 direction = adjustedDirection - transform.position;

		rb.AddForce(direction * ballSettings.speed);

		ballState = BallState.Rolling;


		if(debugBall.ShowNextWaypointLine) Debug.DrawRay(transform.position, direction, Color.green);
		}
    }
   

    public void ResetBall()
    {

    	transform.position = startPosition.transform.position;
    	transform.rotation = startPosition.transform.rotation;

    	rb.velocity = Vector3.zero;
    	rb.angularVelocity = Vector3.zero;

    	currentWaypoint = wpc.FirstWaypoint();

    	xPitch = 0;

    	ballState = BallState.Idle;
    }



    void OnTriggerEnter(Collider col)
    {
    	//Enter OutOfBounds Directly
    	OutOfBounds oob = col.gameObject.GetComponent<OutOfBounds>();

    	if(oob)
    	{
    		ballState = BallState.OutOfBounds;
    	}

    }


    [System.Serializable]
    public class BallSettings
    {
		public float speed;
		public float jumpForce;

    }

    [System.Serializable]
    public class DebugBall
    {
   		public bool ShowBoundsNormalRay;
   		public bool ShowNextWaypointLine;
   		public bool ShowGroundedCheckRay;
    }

}