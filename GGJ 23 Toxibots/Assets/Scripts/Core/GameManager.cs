using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class GameManager
	{
		/// <summary>
		/// Singleton.
		/// </summary>
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
		/// The remanding levels in the game.
		/// </summary>
		private Queue<LevelConfiguration> _LevelsConfigurationBag;

		/// <summary>
		/// LevelManager instance.
		/// </summary>
		private LevelManager _LevelManager;

		/// <summary>
		/// GameManager current state.
		/// </summary>
		private GameManagerState _State;

		/// <summary>
		/// Observers.
		/// </summary>
		private List<IGameManagerListener> _Subscribers;

		/// <summary>
		/// GameManagerMono
		/// </summary>
		private MonoBehaviour _GameManagerMono;

		/// <summary>
		/// Level Manager instance.
		/// </summary>
		public LevelManager LevelManager => _LevelManager;

		/// <summary>
		/// Indicates if the Game is running.
		/// </summary>
		public Boolean IsRunning => _State != GameManagerState.kIdle;

		public MonoBehaviour Mono => _GameManagerMono;

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
		public static GameManager GetInstance()
		{
			return _INSTANCE;
		}

		/// <summary>
		/// Instantiates a GameManager.
		/// </summary>
		private GameManager()
		{
			_LevelManager = new LevelManager();
			_LevelsConfigurationBag = new Queue<LevelConfiguration>();
			_State = GameManagerState.kIdle;
			_Subscribers = new List<IGameManagerListener>();
		}

		public void SetMonobehaviour(MonoBehaviour mono)
		{
			_GameManagerMono = mono;
		}

		/// <summary>
		/// Get the name of all the available levels.
		/// </summary>
		/// 
		/// <returns>List with the name of all the levels available.</returns>
		public List<String> GetLevelsName()
		{
			IEnumerable<LevelConfiguration> levelsConfig = _GameConfiguration.levels;
			return levelsConfig.Select(levelConfig => levelConfig.name).ToList();
		}

		public void StartGame()
		{
			if (_State == GameManagerState.kIdle)
			{	
				SetState(GameManagerState.kInitNextLevel);
				foreach (IGameManagerListener listener in _Subscribers)
				{
					listener.OnGameStarted();
				}
			}
		}

		public void OnUpdate(float deltaTime)
		{
			switch (_State)
			{
				case GameManagerState.kInitNextLevel:
					OnUpdate_InitNextLevel();
					return;
				case GameManagerState.kStartLevel:
					OnUpdate_StartLevel();
					return;
				case GameManagerState.kUpdateLevel:
					OnUpdate_UpdateLevel(deltaTime);
					return;
				case GameManagerState.kEvaluateLevels:
					OnUpdate_EvaluateLevels();
					return;
				default:
					return;
			}
		}

		public void ResetLevels()
		{
			InitGame();
			foreach (IGameManagerListener listener in _Subscribers)
			{
				listener.OnGameReset();
			}
		}

		public void Subscribe(IGameManagerListener listner)
		{
			_Subscribers.Add(listner);
		}

		private void OnStart(String path = null)
		{
			TextAsset gameConfigFile = Resources.Load<TextAsset>(path == null ? _GAME_CONFIG_PATH : path);
			_GameConfiguration = JsonUtility.FromJson<GameConfiguration>(gameConfigFile.text);
			InitGame();
		}

		private void OnShutdown()
		{
			_INSTANCE = null;
		}

		/// <summary>
		/// Get a level configuration.
		/// </summary>
		/// <param name="levelName">Level's name.</param>
		/// 
		/// <returns>LevelConfiguration.</returns>
		private LevelConfiguration GetLevelConfiguration(String levelName)
		{
			foreach (LevelConfiguration levelConfiguration in _GameConfiguration.levels)
			{
				if (levelConfiguration.Equals(levelName))
				{
					return levelConfiguration;
				}
			}
			throw new ArgumentException(String.Format("Level not found: {0}", levelName));
		}

		private void OnUpdate_InitNextLevel()
		{
			if (_LevelsConfigurationBag.Count <= 0)
			{
				throw new Exception("No Levels to load");
			}

			LevelConfiguration nextLevel = _LevelsConfigurationBag.Dequeue();
			_LevelManager.SetActiveLevel(nextLevel);
			foreach (IGameManagerListener listener in _Subscribers)
			{
				listener.OnGameLevelChanged(nextLevel.name);
			}

			_GameManagerMono.StartCoroutine(
				DelayedStateTranstion(
					_GameConfiguration.level_presentation_duration, 
					GameManagerState.kStartLevel
				)
			);
		}

		private void OnUpdate_StartLevel()
		{
			_LevelManager.StartLevel();
			SetState(GameManagerState.kUpdateLevel);
		}

		private void OnUpdate_UpdateLevel(float deltaTime)
		{
			_LevelManager.OnUpdate(deltaTime);
			if (!_LevelManager.IsLevelRunning)
			{
				if (_LevelManager.IsLevelCompleted)
				{
					SetState(GameManagerState.kEvaluateLevels);
				}
				else if (_LevelManager.IsLevelFailed)
				{
					SetState(GameManagerState.kGameFailure);
					foreach (IGameManagerListener listener in _Subscribers)
					{
						listener.OnGameFailed();
					}
				}
				else
				{
					throw new Exception("logic error.");
				}
			}
		}

		private void OnUpdate_EvaluateLevels()
		{
			if (_LevelsConfigurationBag.Count > 0)
			{
				SetState(GameManagerState.kInitNextLevel);
			}
			else
			{
				SetState(GameManagerState.kGameCompleted);
				foreach (IGameManagerListener listener in _Subscribers)
				{
					listener.OnGameCompleted();
				}
			}
		}

		private void SetState(GameManagerState state)
		{
			_State = state;
			foreach (IGameManagerListener listener in _Subscribers)
			{
				listener.OnGameManagerStateChanged(_State);
			}
		}

		private void InitGame()
		{
			_LevelsConfigurationBag = new Queue<LevelConfiguration>(_GameConfiguration.levels);
			SetState(GameManagerState.kIdle);
		}

		private IEnumerator DelayedStateTranstion(float seconds, GameManagerState toState)
		{
			SetState(GameManagerState.kInDelayedTranstion);
			yield return new WaitForSeconds(seconds);
			SetState(toState);
		}
	}
}
