using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {


	ScoreManager score;


	public Text txtPlayerName;
	public Text[] txtFrames;
	public Text[] txtScoreTotals;


	// Use this for initialization
	void Start () {

		score = GameObject.FindObjectOfType<ScoreManager>();

	}
	
	// Update is called once per frame


	public void CleanUpScores()
	{
		
		//txtPlayerName.text = "";

		foreach(Text t in txtFrames)
		{
			t.text = "";
		}
		foreach(Text t in txtScoreTotals)
		{
			t.text = "";
		}
	}

	public void DisplayPlayerName(GameStateManager.PlayerAvailable player)
	{
		int idPlayer = 0;
		switch (player) {

			case GameStateManager.PlayerAvailable.Player1:
				idPlayer = 0;
			break;
			case GameStateManager.PlayerAvailable.Player2:
				idPlayer = 1;
			break;
			case GameStateManager.PlayerAvailable.Player3:
				idPlayer = 2;
			break;
			case GameStateManager.PlayerAvailable.Player4:
				idPlayer = 3;
			break;

		}

		txtPlayerName.text = score.scoreCards[idPlayer].GetPlayerName();
	}

	public void UpdateScore (GameStateManager.PlayerAvailable player) 
	{


		int idPlayer = 0;
		switch (player) {

			case GameStateManager.PlayerAvailable.Player1:
				idPlayer = 0;
			break;
			case GameStateManager.PlayerAvailable.Player2:
				idPlayer = 1;
			break;
			case GameStateManager.PlayerAvailable.Player3:
				idPlayer = 2;
			break;
			case GameStateManager.PlayerAvailable.Player4:
				idPlayer = 3;
			break;

		}

		List<ScoreManager.ScoreCard.Score> scores = score.scoreCards[idPlayer].GetScores();

		txtPlayerName.text = score.scoreCards[idPlayer].GetPlayerName();


	 	CleanUpScores();
	
		int i = 0;

		foreach(ScoreManager.ScoreCard.Score s in scores)
		{
			Text t = txtFrames[i];

			if(s.scoreType == ScoreManager.ScoreCard.Score.ScoreType.Value)
				t.text = s.Value.ToString();
			else if(s.scoreType == ScoreManager.ScoreCard.Score.ScoreType.None || 
				s.scoreType == ScoreManager.ScoreCard.Score.ScoreType.Skip)
				t.text = "-";
			else if(s.scoreType == ScoreManager.ScoreCard.Score.ScoreType.Spare)
				t.text = "/";
			else if(s.scoreType == ScoreManager.ScoreCard.Score.ScoreType.Strike)
				t.text = "X";

			i++;
		}



		List<int> frameTotals = score.scoreCards[idPlayer].GetFrameTotals();

		i = 0;
		foreach(int total in frameTotals)
		{
			Text t = txtScoreTotals[i];

			t.text = total.ToString();

			i++;
		}
		
	}
}
