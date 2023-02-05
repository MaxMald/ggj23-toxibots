using Assets.Scripts;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
	void Awake()
	{
		if (GameManager.GetInstance() == null)
		{
			GameManager.START();
			GameManager.GetInstance().SetMonobehaviour(this);
		}
	}
}