using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class GameManager
	{
		private static GameManager _INSTANCE = null;

		/// <summary>
		/// Relative path to the configuration file.
		/// </summary>
		private static String _GAME_CONFIG_PATH = "Config/GameConfig";

		/// <summary>
		/// The game's configuration.
		/// </summary>
		private GameConfiguration _GameConfiguration;

		/// <summary>
		/// Initializes the GameManager.
		/// </summary>
		public static void START()
		{
			if (_INSTANCE == null)
			{
				_INSTANCE = new GameManager();
				_INSTANCE.OnStart();
			}
		}

		/// <summary>
		/// Clear resources and shutdown the singleton.
		/// </summary>
		public static void SHUTDOWN()
		{
			if (_INSTANCE != null)
			{
				_INSTANCE.OnShutdown();
				_INSTANCE = null;
			}
		}

		/// <summary>
		/// Get the instance singleton.
		/// </summary>
		/// 
		/// <returns>The singleton.</returns>
		public GameManager GetInstance()
		{
			return _INSTANCE;
		}

		private GameManager()
		{ }

		private void OnStart()
		{
			TextAsset gameConfigFile = Resources.Load<TextAsset>(_GAME_CONFIG_PATH);
			_GameConfiguration = JsonUtility.FromJson<GameConfiguration>(gameConfigFile.text);
			var x = 1 - 1;
		}

		private void OnShutdown()
		{
			// do stuff
		}
	}
}
