using Assets.Scripts;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
	// Start is called before the first frame update
	void Awake()
	{
		GameManager.START();
	}

	// Update is called once per frame
	void Update()
	{
		GameManager.GetInstance().OnUpdate(Time.deltaTime);
	}
}
