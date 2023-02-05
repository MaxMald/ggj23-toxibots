using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SymbolsBoardPanelView : MonoBehaviour, ISymbolsBoardListener, ISymbolSignalManagerListener, IGameManagerListener
{
	[SerializeField]
	private TextMeshProUGUI _SymbolsSequenceText;

	public void OnGameCompleted()
	{
		_SymbolsSequenceText.text = "";
	}

	public void OnGameFailed()
	{
		_SymbolsSequenceText.text = "";
	}

	public void OnGameLevelChanged(string levelName)
	{
		_SymbolsSequenceText.text = "";
	}

	public void OnGameManagerStateChanged(GameManagerState newState)
	{ }

	public void OnGameReset()
	{ }

	public void OnGameStarted()
	{ }

	public void OnSymbolBoardCleared()
	{
		//_SymbolsSequenceText.text = "";
	}

	public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols)
	{
		String sequenceText = "";
		foreach (Symbol symbol in visibleSymbols)
		{
			sequenceText += String.Format("{0} - ", symbol.Key.ToString());
		}
		if (sequenceText.Length > 4)
		{
			sequenceText = sequenceText.Substring(0, sequenceText.Length - 3);
		}
		_SymbolsSequenceText.color = Color.white;
		_SymbolsSequenceText.text = sequenceText;
	}

	public void OnSymbolsBoardSequenceCompleted()
	{
		_SymbolsSequenceText.text = "";
	}

	public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
	{ }

	public void OnSymbolSignalReceived(SymbolResponse response)
	{
		_SymbolsSequenceText.color = Color.green;
		if (String.IsNullOrEmpty(_SymbolsSequenceText.text))
		{
			_SymbolsSequenceText.text = response.ReceivedSymbol.Key.ToString();
		}
		else
		{
			_SymbolsSequenceText.text += String.Format(" - {0}", response.ReceivedSymbol.Key.ToString());
		}
	}

	public void OnSymbolSingalManagerClear()
	{
		//_SymbolsSequenceText.text = "";
	}

	// Start is called before the first frame update
	void Start()
	{
		GameManager.GetInstance().Subscribe(this);
		GameManager.GetInstance().LevelManager.SymbolsBoard.Subscribe(this);
		GameManager.GetInstance().LevelManager.SymbolSignalManager.Subscribe(this);
	}

	// Update is called once per frame
	void Update()
	{ }
}
