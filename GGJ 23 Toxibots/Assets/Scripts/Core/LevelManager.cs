using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public class LevelManager
	{
		/// <summary>
		/// The current level configuration.
		/// </summary>
		private LevelConfiguration _LevelConfiguration = null;

		/// <summary>
		/// List of subscribers to the events of this LevelManager.
		/// </summary>
		private List<ILevelManagerListener> _Subscribers;

		/// <summary>
		/// Indicates weather the level is paused or not.
		/// </summary>
		private Boolean _IsPaused;

		/// <summary>
		/// Indicates the state of the level.
		/// </summary>
		private LevelState _State;

		/// <summary>
		/// The Meter instance.
		/// </summary>
		public Meter Meter { get; private set; }

		/// <summary>
		/// The SymbolsBoard instance.
		/// </summary>
		public SymbolsBoard SymbolsBoard { get; private set; }

		/// <summary>
		/// The SymbolSignalManager instance.
		/// </summary>
		public SymbolSignalManager SymbolSignalManager { get; private set; }

		/// <summary>
		/// Get the name of the current level.
		/// </summary>
		public String CurrentLevelName
		{
			get
			{
				return (_LevelConfiguration != null ? _LevelConfiguration.name : "NONAME");
			}
		}

		/// <summary>
		/// Indicates if the user is still playing the level.
		/// </summary>
		public Boolean IsLevelRunning => _State != LevelState.kLevelCompleted && _State != LevelState.kLevelFailure;

		/// <summary>
		/// Indicates if the user has failed the current level.
		/// </summary>
		public Boolean IsLevelFailed => _State == LevelState.kLevelFailure;

		/// <summary>
		/// Indicates if the user has completed the current level.
		/// </summary>
		public Boolean IsLevelCompleted => _State == LevelState.kLevelCompleted;

		/// <summary>
		/// Instantiates a LevelManager.
		/// </summary>
		public LevelManager()
		{	
			SymbolsBoard = new SymbolsBoard();
			SymbolSignalManager = new SymbolSignalManager();
			Meter = new Meter();
			SymbolSignalManager.Subscribe(Meter);
			_IsPaused = false;
			_Subscribers = new List<ILevelManagerListener>();
			SetState(LevelState.kIdle);
		}

		/// <summary>
		/// Initialize the level manager with a new level configuration.
		/// </summary>
		/// <param name="levelConfiguration"></param>
		public void SetActiveLevel(LevelConfiguration levelConfiguration)
		{
			_LevelConfiguration = levelConfiguration;
			InitLevel();
			foreach (ILevelManagerListener subscriber in _Subscribers)
			{
				subscriber.OnLevelChanged(CurrentLevelName);
			}
			SetState(LevelState.kIdle);
		}

		/// <summary>
		/// Starts the level.
		/// </summary>
		public void StartLevel()
		{
			if (_LevelConfiguration == null)
			{
				throw new Exception("No level loaded.");
			}

			if (_State == LevelState.kIdle)
			{
				SetState(LevelState.kGenerateNewSequence);
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelStarted(CurrentLevelName);
				}
			}
		}

		/// <summary>
		/// Reset the current level.
		/// </summary>
		public void Reset()
		{
			InitLevel();
			foreach (ILevelManagerListener subscriber in _Subscribers)
			{
				subscriber.OnLevelReset(CurrentLevelName);
			}
			SetState(LevelState.kIdle);
		}

		/// <summary>
		/// Resumes the level.
		/// </summary>
		public void Resume()
		{
			if (_IsPaused)
			{
				_IsPaused = false;
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelResumed(CurrentLevelName);
				}
			}
		}

		/// <summary>
		/// Pauses the level.
		/// </summary>
		public void Pause()
		{
			if (!_IsPaused)
			{
				_IsPaused = true;
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelPaused(CurrentLevelName);
				}
			}
		}

		/// <summary>
		/// Subscribe to the events of this LevelManager.
		/// </summary>
		/// 
		/// <param name="listener">Observer.</param>
		public void Subscribe(ILevelManagerListener listener)
		{
			_Subscribers.Add(listener);
		}

		/// <summary>
		/// Update.
		/// </summary>
		/// 
		/// <param name="deltaTime">Delta time.</param>
		public void OnUpdate(float deltaTime)
		{
			switch (_State)
			{
				case LevelState.kIdle:
					return;
				case LevelState.kGenerateNewSequence:
					OnUpdate_GenerateNewSequence();
					return;
				case LevelState.kPresentSequence:
					OnUpdate_PresentSequence(deltaTime);
					return;
				case LevelState.kReceiveSequence:
					OnUpdate_ReceiveSequence(deltaTime);
					return;
				case LevelState.kEvaluateSequences:
					OnUpdate_EvaluateSequences();
					return;
				default:
					return;
			}
		}

		/// <summary>
		/// Set the level with its initial values.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void InitLevel()
		{
			if (_LevelConfiguration == null)
			{
				throw new Exception("LevelConfiguratio not loaded");
			}

			Meter.Init(
				_LevelConfiguration.meter_filling_speed,
				_LevelConfiguration.meter_initial_value,
				_LevelConfiguration.wrong_symbol_signal_meter_penalty
			);

			List<Symbol> symbols = new List<Symbol>();
			foreach (String symbolKey in _LevelConfiguration.symbols)
			{
				symbols.Add(new Symbol(Char.Parse(symbolKey)));
			}
			SymbolsBoard.Init(
				symbols,
				_LevelConfiguration.symbol_presentation_step_duration,
				_LevelConfiguration.sequences_size);
			SymbolSignalManager.Clear();
		}

		private void OnUpdate_GenerateNewSequence()
		{
			if (!SymbolsBoard.HasSequences())
			{
				throw new Exception("No Sequences.");
			}

			SymbolsBoard.GenerateNextSequence();
			SymbolSignalManager.Init(new List<Symbol>(SymbolsBoard.SymbolSequence));
			SetState(LevelState.kPresentSequence);
		}

		private void OnUpdate_PresentSequence(float deltaTime)
		{
			if (_IsPaused) return;

			SymbolsBoard.Update(deltaTime);
			if (SymbolsBoard.IsCompleted)
			{
				SetState(LevelState.kReceiveSequence);
			}
		}

		private void OnUpdate_ReceiveSequence(float deltaTime)
		{
			if (_IsPaused) return;

			SymbolSignalManager.OnUpdate();
			if (SymbolSignalManager.IsCompleted)
			{
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelSequenceCompleted(_LevelConfiguration.name);
				}
				SetState(LevelState.kEvaluateSequences);
				return;
			}

			Meter.Update(deltaTime);
			if (Meter.IsFilled())
			{
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelFail(CurrentLevelName);					
				}
				SetState(LevelState.kLevelFailure);
				return;
			}
		}

		private void OnUpdate_EvaluateSequences()
		{
			if (SymbolsBoard.HasSequences())
			{
				SetState(LevelState.kGenerateNewSequence);
			}
			else
			{
				foreach (ILevelManagerListener subscriber in _Subscribers)
				{
					subscriber.OnLevelCompleted(CurrentLevelName);
				}
				SetState(LevelState.kLevelCompleted);
			}
		}

		private void SetState(LevelState state)
		{
			_State = state;
			foreach (ILevelManagerListener subscriber in _Subscribers)
			{
				subscriber.OnLevelManagerStateChanged(_State);
			}
		}
	}
}
