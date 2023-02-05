using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerView : MonoBehaviour, IGameManagerListener, ILevelManagerListener, ISymbolsBoardListener, ISymbolSignalManagerListener
{
	public void OnGameCompleted()
	{ }

	public void OnGameFailed()
	{ }

	public void OnGameLevelChanged(string levelName)
	{}

	public void OnGameManagerStateChanged(GameManagerState newState)
	{}

	public void OnGameReset()
	{}

	public void OnGameStarted()
	{}

	public void OnLevelChanged(string levelName)
	{
		 
	}

	public void OnLevelCompleted(string levelName)
	{
		 
	}

	public void OnLevelFail(string levelName)
	{
		 
	}

	public void OnLevelManagerStateChanged(LevelState state)
	{
		 
	}

	public void OnLevelPaused(string levelName)
	{
		 
	}

	public void OnLevelReset(string levelName)
	{
		 
	}

	public void OnLevelResumed(string levelName)
	{
		 
	}

	public void OnLevelSequenceCompleted(string levelName)
	{
		 
	}

	public void OnLevelStarted(string levelName)
	{
		 
	}

	public void OnSymbolBoardCleared()
	{
		 
	}

	public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols)
	{
		 
	}

	public void OnSymbolsBoardSequenceCompleted()
	{
		 
	}

	public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
	{
		 
	}

	public void OnSymbolSignalReceived(SymbolResponse response)
	{
		 
	}

	public void OnSymbolSingalManagerClear()
	{
		 
	}

	// Start is called before the first frame update
	void Start()
	{
		GameManager gm = GameManager.GetInstance();
		gm.Subscribe(this);
		gm.LevelManager.Subscribe(this);
		gm.LevelManager.SymbolSignalManager.Subscribe(this);
		gm.LevelManager.SymbolsBoard.Subscribe(this);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
