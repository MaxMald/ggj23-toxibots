using Assets.Scripts;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
	void Update()
	{
		GameManager gameManager = GameManager.GetInstance();
		if (!gameManager.IsRunning)
		{
			if (Input.GetKey(KeyCode.Return))
			{
				gameManager.StartGame();
			}
		}
		gameManager.OnUpdate(Time.deltaTime);
	}
}
