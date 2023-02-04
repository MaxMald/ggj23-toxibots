using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggingPanelView : MonoBehaviour, IGameManagerListener, ILevelManagerListener, IMeterListener, ISymbolsBoardListener
{
	[SerializeField]
	private Text _GameManagerStateText;

	[SerializeField]
	private Text _LevelManagerStateText;

	[SerializeField]
	private Text _SymbolBoardManagerSequenceText;

	[SerializeField]
	private Text _MeterAmountText;

	[SerializeField]
	private Text _LevelNameText;

	public void ToggleOnOff()
	{
		gameObject.SetActive(!gameObject.active);
	}

	public void OnLevelChanged(string levelName)
	{
		_LevelNameText.text = levelName;
	}

	public void OnLevelCompleted(string levelName)
	{ }

	public void OnLevelFail(string levelName)
	{ }

	public void OnLevelManagerStateChanged(LevelState state)
	{
		_LevelManagerStateText.text = state.ToString();
	}

	public void OnLevelPaused(string levelName)
	{ }

	public void OnLevelReset(string levelName)
	{ }

	public void OnLevelResumed(string levelName)
	{ }

	public void OnLevelSequenceCompleted(string levelName)
	{ }

	public void OnLevelStarted(string levelName)
	{ }

	public void OnMeterAmountChanged(float meterAmount)
	{
		_MeterAmountText.text = String.Format("{0} secs.", meterAmount);
	}

	public void OnSymbolBoardCleared()
	{ }

	public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols)
	{
		String msg = "";
		foreach (Symbol symbol in visibleSymbols)
		{
			msg += symbol.Key.ToString() + " ";
		}
		_SymbolBoardManagerSequenceText.text = msg;
	}

	public void OnSymbolsBoardSequenceCompleted()
	{ }

	void IGameManagerListener.OnGameCompleted()
	{ }

	void IGameManagerListener.OnGameFailed()
	{ }

	void IGameManagerListener.OnGameLevelChanged(string levelName)
	{
		_LevelNameText.text = levelName;
	}

	void IGameManagerListener.OnGameManagerStateChanged(GameManagerState newState)
	{
		_GameManagerStateText.text = newState.ToString();
	}

	void IGameManagerListener.OnGameReset()
	{ }

	void IGameManagerListener.OnGameStarted()
	{ }

	// Start is called before the first frame update
	void Start()
	{
		GameManager gm = GameManager.GetInstance();
		gm.Subscribe(this);
		gm.LevelManager.Subscribe(this);
		gm.LevelManager.SymbolsBoard.Subscribe(this);
		gm.LevelManager.Meter.Subscribe(this);
	}

	// Update is called once per frame
	void Update()
	{ }
}
