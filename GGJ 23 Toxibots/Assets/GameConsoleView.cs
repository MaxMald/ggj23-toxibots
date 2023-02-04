using Assets.Scripts;
using Assets.Scripts.ConsoleView;
using UnityEngine;

public class GameConsoleView : MonoBehaviour
{
	[SerializeField]
	MeterTextView meterTextView;

	[SerializeField]
	bool logGameManager = true;

	[SerializeField]
	bool logLevelManager = true;

	[SerializeField]
	bool logSymbolSignalManager = true;

	[SerializeField]
	bool logSymbolsBoardManager = true;

	// Start is called before the first frame update
	void Start()
    {
		GameManager gameManager = GameManager.GetInstance();
		gameManager.Subscribe(new GameManagerConsoleView(logGameManager));
		gameManager.LevelManager.Subscribe(new LevelManagerConsoleView(logLevelManager));
		gameManager.LevelManager.SymbolsBoard.Subscribe(new SymbolsBoardManagerConsoleView(logSymbolsBoardManager));
		gameManager.LevelManager.SymbolSignalManager.Subscribe(new SymbolSignalManagerConsoleView(logSymbolSignalManager));
		gameManager.LevelManager.Meter.Subscribe(meterTextView);
		gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
