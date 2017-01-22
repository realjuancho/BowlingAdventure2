using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


	public ScoreCard[] scoreCards;

	bool scoreFull;
	public ScoreCard.Score.ScoreType lastScoreType;
	public ScoreCard.Score.FrameType lastFrameType;

	void Start()
	{
	}


	public void CleanScores()
	{
		foreach(ScoreCard sc in scoreCards)
		{
			sc.CleanScores();
		} 
	}

	public void PushScore(int score, GameStateManager.PlayerAvailable player)
	{

		ScoreCard.Score s = new ScoreCard.Score();
		s.Value = score;

		//Select the player that the score belongs to
		int scoreCardId = 0;
		switch(player)
		{
			case GameStateManager.PlayerAvailable.Player1:
				scoreCardId = 0;
			break;

			case GameStateManager.PlayerAvailable.Player2:
				scoreCardId = 1;
			break;

			case GameStateManager.PlayerAvailable.Player3:
				scoreCardId = 2;
			break;

			case GameStateManager.PlayerAvailable.Player4:
				scoreCardId = 3;
			break;
		}

		//Determine if it's the first score entered
		int scoreCardPointer = 0;

		List<ScoreCard.Score> scores = scoreCards[scoreCardId].GetScores();

	
		if (scores != null)
		{
			scoreCardPointer = scores.Count;
		}

		//Determine if score is for the first or second frame
		if(scoreCardPointer % 2 == 0)
		{
			s.frameType = ScoreCard.Score.FrameType.First;
		}
		else 
		{
			s.frameType = ScoreCard.Score.FrameType.Second;
		}

		if(scoreCardPointer == 20)
		{
			s.frameType = ScoreCard.Score.FrameType.Extra;
		}

		if(scoreCardPointer > 17)
		{
			s.InLastFrame = true;
		}

		//Determine if it was a Spare
		switch(s.frameType)	
		{
			case ScoreCard.Score.FrameType.First:

				//10 PINS Defeated
				if(score == 10) s.scoreType = ScoreCard.Score.ScoreType.Strike;
				else if(score == 0) s.scoreType = ScoreCard.Score.ScoreType.None;
				else s.scoreType = ScoreCard.Score.ScoreType.Value;
				break;

			case ScoreCard.Score.FrameType.Second:

				int previousScore = scores[scores.Count -1].Value;

				//Previous frame + current = 10 PINS
				if(s.InLastFrame && score == 10) s.scoreType = ScoreCard.Score.ScoreType.Strike;
				else if(score + previousScore == 10) s.scoreType = ScoreCard.Score.ScoreType.Spare;
				else if(score == 0) s.scoreType = ScoreCard.Score.ScoreType.None;
				else s.scoreType = ScoreCard.Score.ScoreType.Value;
			
				break;

			case ScoreCard.Score.FrameType.Extra:

				if(score == 10) s.scoreType = ScoreCard.Score.ScoreType.Strike;
				else if(score == 0) s.scoreType = ScoreCard.Score.ScoreType.None;
				else s.scoreType = ScoreCard.Score.ScoreType.Value;
				break;
		}		




		//Add the score to the cards
		scores.Add(s);

		if(s.scoreType == ScoreCard.Score.ScoreType.Strike && !s.InLastFrame)
		{
			ScoreCard.Score skipScore = new ScoreCard.Score();
			skipScore.scoreType = ScoreCard.Score.ScoreType.Skip;
			skipScore.frameType = ScoreCard.Score.FrameType.Second;
			skipScore.Value = 0;

			scores.Add(skipScore);
		}


		lastScoreType = s.scoreType;
		lastFrameType = s.frameType;
	}


	public bool IsScoreFull()
	{
		foreach(ScoreCard sc in scoreCards)
		{
			if(!sc.ScoreComplete())
				return false;
		}

		return true;
	}

	public void InitializeScoreCard(SettingsManager.GameSettings.PlayersSelection playersSelected, string[] playerNames)
	{
		if(playersSelected == SettingsManager.GameSettings.PlayersSelection.OnePlayer)
		{
			scoreCards = new ScoreCard[]{
				new ScoreCard(playerNames[0], GameStateManager.PlayerAvailable.Player1)
			};
		}
		else if (playersSelected == SettingsManager.GameSettings.PlayersSelection.TwoPlayers)
		{
			scoreCards = new ScoreCard[]{
				new ScoreCard(playerNames[0], GameStateManager.PlayerAvailable.Player1),
				new ScoreCard(playerNames[1], GameStateManager.PlayerAvailable.Player2)
			};
		}
		else if (playersSelected == SettingsManager.GameSettings.PlayersSelection.ThreePlayers)
		{
			scoreCards = new ScoreCard[]{
				new ScoreCard(playerNames[0], GameStateManager.PlayerAvailable.Player1), 
				new ScoreCard(playerNames[1], GameStateManager.PlayerAvailable.Player2),
				new ScoreCard(playerNames[2], GameStateManager.PlayerAvailable.Player3)
			};
		}
		else if (playersSelected == SettingsManager.GameSettings.PlayersSelection.FourPlayers)
		{
			scoreCards = new ScoreCard[]{
				new ScoreCard(playerNames[0], GameStateManager.PlayerAvailable.Player1),
				new ScoreCard(playerNames[1], GameStateManager.PlayerAvailable.Player2),
				new ScoreCard(playerNames[2], GameStateManager.PlayerAvailable.Player3),
				new ScoreCard(playerNames[3], GameStateManager.PlayerAvailable.Player4)
			};
		}
	}


	[System.Serializable]
	public class ScoreCard
	{

		List<Score> scores = new List<Score>();


		GameStateManager.PlayerAvailable player;
		string PlayerName;


		public void SetPlayerName(string playerName)
		{
			PlayerName = playerName;
		}

		public string GetPlayerName()
		{
			return PlayerName;
		}

		public ScoreCard(string playerName, GameStateManager.PlayerAvailable player)
		{
			SetPlayerName(playerName);
		}

		public List<Score> GetScores()
		{
			return scores;
		}

		List<int> FrameTotals = new List<int>();

		public bool ExtraBallAwarded;

		public List<int> GetFrameTotals()
		{
			
			int tmpValue=0;
			int totalValue = 0;

			FrameTotals.Clear();
			foreach(Score s in scores)
			{
				if(s.scoreType != Score.ScoreType.Skip)
				{
					if(s.frameType == Score.FrameType.First && !s.InLastFrame)
					{
						tmpValue += s.Value;
						if(s.scoreType == Score.ScoreType.Strike)
						{
							int currentId = scores.IndexOf(s);
							if(scores.Count > currentId +2)
							{
								Score nextScore = scores[currentId +2];

				
								tmpValue+= nextScore.Value;

								if(nextScore.scoreType == Score.ScoreType.Strike)
								{
									if(scores.Count > currentId +4)
									{
										Score next2Scores = scores[currentId +4];

										tmpValue += next2Scores.Value;
									}
								}
								else if(nextScore.scoreType != Score.ScoreType.Strike)
								{
									if(scores.Count > currentId +3)
									{
										Score next2Scores = scores[currentId +3];

										tmpValue += next2Scores.Value;
									}

								}

							}

							 totalValue+= tmpValue;
							 FrameTotals.Add(totalValue);
							 tmpValue = 0;
						}
					}
					else if(s.frameType == Score.FrameType.Second && !s.InLastFrame)
						{
							tmpValue += s.Value;
							if(s.scoreType == Score.ScoreType.Spare)
							{
								int currentID = scores.IndexOf(s);

								if (scores.Count > currentID+1)
								{
									tmpValue += scores[currentID +1].Value;
								}
							}
							totalValue += tmpValue;
						
							FrameTotals.Add(totalValue);
							tmpValue = 0;
						}
					else if(s.InLastFrame)
					{
						
						if(s.frameType == Score.FrameType.First)
						{
							tmpValue += s.Value;

						}
						else if(s.frameType == Score.FrameType.Second)
						{
							tmpValue += s.Value;

							if(tmpValue >= 10) 
							{
								ExtraBallAwarded = true;
							}
							else
							{
								totalValue += tmpValue;
								FrameTotals.Add(totalValue);
								tmpValue = 0;
							}

						}
						else if(s.frameType == Score.FrameType.Extra)
						{
							tmpValue+= s.Value;
							totalValue += tmpValue;
							FrameTotals.Add(totalValue);
							tmpValue = 0;
						}


					}
				}
			}
		
			return FrameTotals;
		}

		public void CleanScores()
		{
			scores.Clear();
		}

		public bool ScoreComplete()
		{

			if (scores.Count == 21 || (scores.Count == 20 && !ExtraBallAwarded ))
				return true;
			else
				return false;

		}

		[System.Serializable]
		public class Score
		{

			public enum FrameType { None, First, Second, Extra };
			public enum ScoreType { None, Value, Spare, Strike, Skip  };

			public bool InLastFrame;
			public FrameType frameType;
			public ScoreType scoreType;
			public int Value;
		}



	}
}
