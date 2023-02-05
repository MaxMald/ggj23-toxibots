using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerView : MonoBehaviour, IGameManagerListener, ILevelManagerListener, ISymbolsBoardListener, ISymbolSignalManagerListener
{
	public AudioSource source;
	public AudioClip successSymbolSound, failSymbolSound;
	public float volume=0.5f;

	public void OnGameCompleted()
	{ }

	public void OnGameFailed()
	{ }

	//No
	public void OnGameLevelChanged(string levelName)
	{}

	//No
	public void OnGameManagerStateChanged(GameManagerState newState)
	{}

	//No
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

	//TODO if lives are created no sound 
	public void OnLevelFail(string levelName)
	{
		 
	}

	//No
	public void OnLevelManagerStateChanged(LevelState state)
	{
		 
	}

	public void OnLevelPaused(string levelName)
	{
		 
	}

	//No
	public void OnLevelReset(string levelName)
	{
		 
	}

	public void OnLevelResumed(string levelName)
	{
		 
	}

	//No
	public void OnLevelSequenceCompleted(string levelName)
	{
		 
	}

	public void OnLevelStarted(string levelName)
	{
		 
	}

	//Maybe
	public void OnSymbolBoardCleared()
	{
		 
	}

	//Cuando aparece en pantalla.
	public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols)
	{
		 
	}

	//Maybe
	public void OnSymbolsBoardSequenceCompleted()
	{
		 
	}

	//No
	public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
	{
		 
	}

	//Este es cuando se envia un sonido y te dice 
	public void OnSymbolSignalReceived(SymbolResponse response)
	{
		if(response.IsCorrect)
		{
			source.PlayOneShot(successSymbolSound, volume);
		}else{
			source.PlayOneShot(failSymbolSound, volume);
		}
	}

	//No
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
