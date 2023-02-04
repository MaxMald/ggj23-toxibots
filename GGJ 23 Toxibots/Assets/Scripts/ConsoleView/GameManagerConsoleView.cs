using System;
using UnityEngine;

namespace Assets.Scripts.ConsoleView
{
	public class GameManagerConsoleView : IGameManagerListener
	{
		private bool show;

		public GameManagerConsoleView(bool show)
		{
			this.show = show;
		}

		public void OnGameCompleted()
		{
			if (show)
				Debug.Log("GAME MANAGER: -- THE GAME HAS BEEN COMPLETED --");
		}

		public void OnGameFailed()
		{
			if (show)
				Debug.Log("GAME MANAGER: -- YOU HAVE LOST THE GAME --");
		}

		public void OnGameLevelChanged(string levelName)
		{
			if (show)
				Debug.LogFormat("GAME MANAGER: New Level : {0}", levelName);
		}

		public void OnGameManagerStateChanged(GameManagerState newState)
		{
			if (show)
				Debug.LogFormat("GAME MANAGER: STATE BEGIN : {0}", newState.ToString());
		}

		public void OnGameReset()
		{
			if (show)
				Debug.Log("GAME MANAGER: Game has been reset.");
		}

		public void OnGameStarted()
		{
			if (show)
				Debug.Log("GAME MANAGER: Game has started.");
		}
	}
}
