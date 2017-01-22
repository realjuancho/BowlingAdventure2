using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {


	public float timeToCountDefeatedPins = 4.0f;
	public PlayerAvailable currentPlayer;
	public GameState gameState;
	public SettingsManager.GameSettings gameSettings;
	public DebugGameManager debugGameManager;

	PinSet pinSet;
	BallControl ball;
	float timeSinceBallInGoal=0.0f;
	ScoreManager scoreManager;
	ScoreUI scoreUI;

	public enum PlayerAvailable { Player1, Player2, Player3, Player4 }

	public enum GameState { 

			WaitingToRollBall,
			WaitingNextPlayer, 
			BallRolling,
			EndOfGame, 
			Pause 

		}

	void Awake()
	{
		scoreManager = GetComponent<ScoreManager>();
		pinSet = GameObject.FindObjectOfType<PinSet>();
		ball = GameObject.FindObjectOfType<BallControl>();
		scoreUI = GameObject.FindObjectOfType<ScoreUI>();
	}

	void Start()
	{
		currentPlayer = PlayerAvailable.Player1;

		gameSettings.InitializePlayerNames();
		scoreManager.InitializeScoreCard(gameSettings.playersSelected, gameSettings.playerNames);

		scoreUI.CleanUpScores();
		scoreUI.DisplayPlayerName(currentPlayer);


	}

	void Update()
	{
		GameStateHelper();

		DebugLoop();
	}

	int LastPinCount = 0;
	void GameStateHelper()
	{
		//If Game hasn't ended
		if(gameState != GameState.EndOfGame)
		{
			
			if(ball.ballState == BallControl.BallState.OutOfBounds)
			{
				

				scoreManager.PushScore(0, currentPlayer);

				if(scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Second)
					pinSet.ResetPins();

				scoreUI.UpdateScore(currentPlayer);

				ball.ResetBall();

				//Check if needs to move to Next Player

				if(gameSettings.PlayersSelected != SettingsManager.GameSettings.PlayersSelection.OnePlayer)
					{
						//Moves to next player if last score was the second attempt 
						//was the extra shot, or was a Strike
						if(scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Second
							|| scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Extra
							|| scoreManager.lastScoreType == ScoreManager.ScoreCard.Score.ScoreType.Strike
							)
						{
							MoveToNextPlayer();
						}
					}
			}

			//Checks if the ball is in the goal area, if so the timer to count the standing pins starts
			if(pinSet.BallInGoal)
			{
				
				timeSinceBallInGoal += Time.deltaTime;
			

				//Checks if time has come to count the standingPins;
				if(timeSinceBallInGoal >= timeToCountDefeatedPins)
				{
					timeSinceBallInGoal = 0.0f;

					int PinCount = pinSet.DefeatedPins - LastPinCount;

					//Send score to ScoreManager
					scoreManager.PushScore(PinCount, currentPlayer);
				
					scoreUI.UpdateScore(currentPlayer);

				

					if(scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.First && scoreManager.lastScoreType 
						!= ScoreManager.ScoreCard.Score.ScoreType.Strike)
					{
						LastPinCount = PinCount;
					}
					else
					{
						LastPinCount = 0;
					}


					if(scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Second
						|| scoreManager.lastScoreType == ScoreManager.ScoreCard.Score.ScoreType.Strike
						)
						pinSet.ResetPins();
					else
						pinSet.CleanPins();

					ball.ResetBall();	

					//Check if needs to move to Next Player

					if(gameSettings.PlayersSelected != SettingsManager.GameSettings.PlayersSelection.OnePlayer)
					{
					//Moves to next player if last score was the second attempt 
					//was the extra shot, or was a Strike
					if(scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Second
						|| scoreManager.lastFrameType == ScoreManager.ScoreCard.Score.FrameType.Extra
						|| scoreManager.lastScoreType == ScoreManager.ScoreCard.Score.ScoreType.Strike
						)
					{
						MoveToNextPlayer();
					}
				}
				}
			}

			else
			{
				timeSinceBallInGoal = 0.0f;
			}



			//if no more shots left for the scoremanager to allocate shots
			if(scoreManager.IsScoreFull()) 
				gameState = GameState.EndOfGame;
		}
		else
		{
			
		}

	}

	void MoveToNextPlayer()
	{

		switch(gameSettings.playersSelected)
		{
			case SettingsManager.GameSettings.PlayersSelection.OnePlayer:
				currentPlayer = PlayerAvailable.Player1;
			break;

		case SettingsManager.GameSettings.PlayersSelection.TwoPlayers:
				if(currentPlayer == PlayerAvailable.Player1)
					currentPlayer = PlayerAvailable.Player2;
				else
					currentPlayer = PlayerAvailable.Player1;

			break;


		case SettingsManager.GameSettings.PlayersSelection.ThreePlayers:
				if(currentPlayer == PlayerAvailable.Player1)
					currentPlayer = PlayerAvailable.Player2;

				else if(currentPlayer == PlayerAvailable.Player2)
					currentPlayer = PlayerAvailable.Player3;

				else if(currentPlayer == PlayerAvailable.Player3)
					currentPlayer = PlayerAvailable.Player1;
			break;


		case SettingsManager.GameSettings.PlayersSelection.FourPlayers:
				
				if(currentPlayer == PlayerAvailable.Player1)
					currentPlayer = PlayerAvailable.Player2;

				else if(currentPlayer == PlayerAvailable.Player2)
					currentPlayer = PlayerAvailable.Player3;

				else if(currentPlayer == PlayerAvailable.Player3)
					currentPlayer = PlayerAvailable.Player4;

				else if( currentPlayer == PlayerAvailable.Player4)
					currentPlayer = PlayerAvailable.Player1;
			break;
		}

		scoreUI.UpdateScore(currentPlayer);
	}

	void DebugLoop()
	{
		if(debugGameManager.initializeGame) 
		{
			gameSettings.InitializePlayerNames();
			scoreManager.InitializeScoreCard(gameSettings.PlayersSelected, gameSettings.playerNames);
			debugGameManager.initializeGame = false;
		}

		if(debugGameManager.initializeScoreCard)
		{
			scoreManager.InitializeScoreCard(gameSettings.playersSelected, gameSettings.playerNames);
			debugGameManager.initializeScoreCard = false;
		}

		if(debugGameManager.moveCurrentPlayer)
		{
			currentPlayer ++;
			debugGameManager.moveCurrentPlayer = false;
		}

		if(debugGameManager.debugScore) 
		{
			scoreUI.CleanUpScores();
			foreach(int s in debugGameManager.DebugScoreValues)
			{
				scoreManager.PushScore(s, currentPlayer);
			}
			scoreUI.UpdateScore(currentPlayer);
			debugGameManager.debugScore = false;
		}
	}




	[System.Serializable]
	public class DebugGameManager
	{
		public bool initializeGame;
		public bool initializeScoreCard;

		public bool moveCurrentPlayer;

		public bool debugScore = true;

		//public int[] DebugScoreValues = new int[] { 8,2,1,4,2,3,5,4,10,9,1,10,10,4,5,10,10 };
		//public int[] DebugScoreValues = new int[] { 10,10,10,10,10,10,10,10,10,10,10,10 };
		public int[] DebugScoreValues = new int[] { 2,4,4,6,2,7,2,8,10,9,1,0,1,5,5,5,5,4,6,4 };
	}


}
