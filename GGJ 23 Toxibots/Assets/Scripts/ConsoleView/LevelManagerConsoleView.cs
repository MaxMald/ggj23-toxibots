using System;
using UnityEngine;

namespace Assets.Scripts.ConsoleView
{
	public class LevelManagerConsoleView : ILevelManagerListener
	{
		private bool _show;

		public LevelManagerConsoleView(bool show)
		{
			_show = show;
		}

		public void OnLevelChanged(string levelName)
		{
			if(_show)
				Debug.LogFormat("LEVEL MANAGER: Level has changed: {0}", levelName);
		}

		public void OnLevelCompleted(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level has been completed: {0}", levelName);
		}

		public void OnLevelFail(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: User has lost the level: {0}", levelName);
		}

		public void OnLevelManagerStateChanged(LevelState state)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: STATE BEGIN: {0}", state.ToString());
		}

		public void OnLevelPaused(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level Paused: {0}", levelName);
		}

		public void OnLevelReset(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level Reset: {0}", levelName);
		}

		public void OnLevelResumed(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level Resumed: {0}", levelName);
		}

		public void OnLevelSequenceCompleted(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level Sequence Completed: {0}", levelName);
		}

		public void OnLevelStarted(string levelName)
		{
			if (_show)
				Debug.LogFormat("LEVEL MANAGER: Level Started: {0}", levelName);
		}
	}
}
