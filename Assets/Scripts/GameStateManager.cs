using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {


	public float timeToCountPins = 2.0f;

	PinSet pinSet;
	BallControl ball;
	float timeSinceBallInGoal=0.0f;

	void Awake()
	{
		pinSet = GameObject.FindObjectOfType<PinSet>();
		ball = GameObject.FindObjectOfType<BallControl>();
	}


	void Update()
	{
		if(ball.ballState == BallControl.BallState.OutOfBounds)
		{
			pinSet.ResetPins();
			ball.ResetBall();
		}

		if(pinSet.BallInGoal)
		{
			timeSinceBallInGoal += Time.deltaTime;
		}
		else
		{
			timeSinceBallInGoal = 0.0f;
		}

		if(timeSinceBallInGoal >= timeToCountPins)
		{
			timeSinceBallInGoal = 0.0f;
			pinSet.ResetPins();
			ball.ResetBall();	
		}
	}

	public class ScoreCard
	{

		public string PlayerName;
		public Score[] scores;
		public int[] scoreFrames;


		public class Score
		{
			public enum ScoreType { None, Value, Spare, Strike  };
			public enum FrameType { Low, High, Extra };

			public FrameType frameType;
			public ScoreType scoreType;
			public int Value;
		}



	}
}
