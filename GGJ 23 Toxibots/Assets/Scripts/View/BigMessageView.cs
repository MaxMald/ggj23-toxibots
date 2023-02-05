using Assets.Scripts;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BigMessageView : MonoBehaviour, IGameManagerListener, ILevelManagerListener
{
	private TextMeshProUGUI _MessageText;

	public void OnGameCompleted()
	{
		_MessageText.text = "¡You saved the forest, you are a winner!";
	}

	public void OnGameFailed()
	{
		_MessageText.text = "¡The forest is dead because of you!";
	}

	public void OnGameLevelChanged(string levelName)
	{
		_MessageText.text = levelName;
	}

	public void OnGameManagerStateChanged(GameManagerState newState)
	{ }

	public void OnGameReset()
	{ }

	public void OnGameStarted()
	{ }

	public void OnLevelChanged(string levelName)
	{ }

	public void OnLevelCompleted(string levelName)
	{
		_MessageText.text = String.Format("{0} COMPLETED", levelName);
	}

	public void OnLevelFail(string levelName)
	{ }

	public void OnLevelManagerStateChanged(LevelState state)
	{
		switch (state)
		{
			case LevelState.kIdle:
				_MessageText.text = "";
				return;
			case LevelState.kPresentSequence:
				_MessageText.text = "";
				return;
		}
	}

	public void OnLevelPaused(string levelName)
	{
		_MessageText.text = "GAME PAUSED";
	}

	public void OnLevelReset(string levelName)
	{ }

	public void OnLevelResumed(string levelName)
	{
		_MessageText.text = "";
	}

	public void OnLevelSequenceCompleted(string levelName)
	{ }

	public void OnLevelStarted(string levelName)
	{
		_MessageText.text = "GAME PAUSED";
	}

	// Start is called before the first frame update
	void Start()
    {
		_MessageText = GetComponent<TextMeshProUGUI>();
		GameManager.GetInstance().Subscribe(this);
		GameManager.GetInstance().LevelManager.Subscribe(this);
	}

    // Update is called once per frame
    void Update()
    { }
}
