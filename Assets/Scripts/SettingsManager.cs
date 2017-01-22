using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
	


	public GameSettings gameSettings;


	public enum SettingMenu { Mode, StoryMode, BallSelect, LevelSelect, SlotSelect, PlayersSelect   }

	public void setGameSetting(SettingMenu setting, string Value)
	{

		switch(setting)
		{
			//public enum SelectedMode { Story, Options, FreePlay, Credits }
			case SettingMenu.Mode:
			if(Value.Equals("")) gameSettings.selectedMode = GameSettings.SelectedMode.None;
					switch(Value)
				{
					case "Story":
						gameSettings.selectedMode = GameSettings.SelectedMode.Story;
					break;
					case "Options":
						gameSettings.selectedMode = GameSettings.SelectedMode.Options;
					break;
					case "FreePlay":
						gameSettings.selectedMode = GameSettings.SelectedMode.FreePlay;
					break;
					case "Credits":
						gameSettings.selectedMode = GameSettings.SelectedMode.Credits;
					break;

				}
			break;



						
			//public enum SlotSelected { Slot01, Slot02, Slot03 }
			case SettingMenu.SlotSelect:
			if(Value.Equals("")) 
				{
					gameSettings.slotSelected = GameSettings.SlotSelected.None;
					gameSettings.storyMode = GameSettings.StoryMode.None;
				}
			else
				{

				string[] Values = Value.Split('_');

				string storyModeValue = Values[0];
				string slotValue = Values[1];
				switch(storyModeValue)
				{
					case "NewGame":
						gameSettings.storyMode = GameSettings.StoryMode.NewGame;

					break;

					case "Continue":
						gameSettings.storyMode = GameSettings.StoryMode.Continue;
					break;
				}

				switch(slotValue)
				{
					case "Slot01":
						gameSettings.slotSelected = GameSettings.SlotSelected.Slot01;
					break;

					case "Slot02":
						gameSettings.slotSelected = GameSettings.SlotSelected.Slot02;
					break;
					case "Slot03":
						gameSettings.slotSelected = GameSettings.SlotSelected.Slot03;
					break;
				}


			}
			break;	

			//public enum SelectedBall { Ball01, Ball02, Ball03, Ball04, Ball05, Ball06, Ball07, Ball08, Ball09, Ball10, Ball11, Ball12 }

			case SettingMenu.BallSelect:

				if(Value.Equals("")) 
				{
					gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.None;
					gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.None;
					gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.None;
					gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.None;
				}
				else
				{
					string[] Values = Value.Split('_');

					string playerValue = Values[0];
					string ballValue = Values[1];

					switch(ballValue)
					{
						case "Ball01":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball01;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball01;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball01;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball01;
						break;

						case "Ball02":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball02;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball02;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball02;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball02;
						break;

						case "Ball03":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball03;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball03;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball03;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball03;
						break;

				case "Ball04":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball04;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball04;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball04;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball04;
						break;

				case "Ball05":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball05;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball05;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball05;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball05;
						break;

				case "Ball06":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball06;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball06;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball06;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball06;
						break;

				case "Ball07":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball07;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball07;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball07;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball07;
						break;

						case "Ball08":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball08;
							if(playerValue == "Player2")
								gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball08;
							if(playerValue == "Player3")
								gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball08;
							if(playerValue == "Player4")
								gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball08;
						break;

				case "Ball09":
						if(playerValue == "Player1")
							gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball09;
						if(playerValue == "Player2")
							gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball09;
						if(playerValue == "Player3")
							gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball09;
						if(playerValue == "Player4")
							gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball09;
					break;

				case "Ball10":
							if(playerValue == "Player1")
						gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball10;
							if(playerValue == "Player2")
						gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball10;
							if(playerValue == "Player3")
						gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball10;
							if(playerValue == "Player4")
						gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball10;
						break;

				case "Ball11":
							if(playerValue == "Player1")
								gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball11;
							if(playerValue == "Player2")
						gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball11;
							if(playerValue == "Player3")
						gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball11;
							if(playerValue == "Player4")
						gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball11;
						break;

				case "Ball12":
							if(playerValue == "Player1")
						gameSettings.selectedBallPlayer1 = GameSettings.SelectedBall.Ball12;
							if(playerValue == "Player2")
						gameSettings.selectedBallPlayer2 = GameSettings.SelectedBall.Ball12;
							if(playerValue == "Player3")
						gameSettings.selectedBallPlayer3 = GameSettings.SelectedBall.Ball12;
							if(playerValue == "Player4")
						gameSettings.selectedBallPlayer4 = GameSettings.SelectedBall.Ball12;
						break;

						default:

						break;
					}
				}
			break;


			//public enum LevelSelected { Level01, Level02, Level03, Level04, Level06, Level07, Level08, Level09, Level10 }
			case SettingMenu.LevelSelect:
			if(Value.Equals("")) gameSettings.levelSelected = GameSettings.LevelSelected.None;
			else
				switch(Value)
				{
					case "Level01":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level01;
						break;
					case "Level02":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level02;
						break;
					case "Level03":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level03;
						break;
					case "Level04":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level04;
						break;
					case "Level05":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level05;
						break;
					case "Level06":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level06;
						break;
					case "Level07":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level07;
						break;
					case "Level08":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level08;
						break;
					case "Level09":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level09;
						break;
					case "Level10":
						gameSettings.levelSelected = GameSettings.LevelSelected.Level10;
						break;
				}

			break;
			//public enum PlayersSelection { None, OnePlayer, TwoPlayers, ThreePlayers, FourPlayers }
			case SettingMenu.PlayersSelect:
				if(Value.Equals("")) gameSettings.playersSelected = GameSettings.PlayersSelection.None;
				else
				switch(Value)
				{
					case "OnePlayer":
						gameSettings.playersSelected = GameSettings.PlayersSelection.OnePlayer;
						break;
					case "TwoPlayers":
						gameSettings.playersSelected = GameSettings.PlayersSelection.TwoPlayers;
						break;
					case "ThreePlayers":
						gameSettings.playersSelected = GameSettings.PlayersSelection.ThreePlayers;
						break;
					case "FourPlayers":
						gameSettings.playersSelected = GameSettings.PlayersSelection.FourPlayers;
						break;
				}
				
			break;
		}
	 
	}

	[System.Serializable]
	public class GameSettings
	{

		public string[] playerNames; 


		public PlayersSelection PlayersSelected {
			get {
				return playersSelected;
			}
			set{
				InitializePlayerNames();
			}
		}

		public void InitializePlayerNames()
		{
			if(playersSelected == PlayersSelection.OnePlayer)
				playerNames = new string[1] { "Player 1" };
			if(playersSelected == PlayersSelection.TwoPlayers)
				playerNames = new string[2] { "Player 1", "Player 2"};
			if(playersSelected == PlayersSelection.ThreePlayers)
				playerNames = new string[3] { "Player 1", "Player 2", "Player 3" };
			if(playersSelected == PlayersSelection.FourPlayers)
				playerNames = new string[4] { "Player 1", "Player 2", "Player 3","Player 4" };
		}


		public SelectedMode selectedMode;
		public StoryMode storyMode;
		public SelectedBall selectedBallPlayer1;
		public SelectedBall selectedBallPlayer2;
		public SelectedBall selectedBallPlayer3;
		public SelectedBall selectedBallPlayer4;
		public LevelSelected levelSelected;
		public SlotSelected slotSelected;
		public PlayersSelection playersSelected;

		public enum SelectedMode { None, Story, Options, FreePlay, Credits }
		public enum StoryMode { None, NewGame, Continue }
		public enum SlotSelected { None, Slot01, Slot02, Slot03 }
		public enum SelectedBall { None, Ball01, Ball02, Ball03, Ball04, Ball05, Ball06, Ball07, Ball08, Ball09, Ball10, Ball11, Ball12 }
		public enum LevelSelected { None, Level01, Level02, Level03, Level04, Level05, Level06, Level07, Level08, Level09, Level10 }
		public enum PlayersSelection { None, OnePlayer, TwoPlayers, ThreePlayers, FourPlayers }

	}

	[System.Serializable]
	public class SaveFile
	{

		[System.Serializable]
		public class GameSave{

			public float timeElapsed;
			public bool[] unlockedLevels  = new  bool[] { true, false, false, false, false, false, false, false, false, false  };
			public bool[] unlockedBalls  = new  bool[] { true, false, false, false, false, false, false, false, false, false, false, false  };
			public int collectedMoney;


		}

	
	}

	[System.Serializable]
	public class PlayerPreferences {

			public int MusicVolume;
			public float EffectsVolume;
	}
}
