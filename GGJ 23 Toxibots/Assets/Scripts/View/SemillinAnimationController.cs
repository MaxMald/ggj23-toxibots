using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SemillinAnimationController : MonoBehaviour, ISymbolSignalManagerListener
{
	[SerializeField]
	private Animator _Animatior;

	public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
	{

	}

	public void OnSymbolSignalReceived(SymbolResponse response)
	{
		if (response.ReceivedSymbol.Key == 'W')
		{
			_Animatior.SetTrigger("W");
		}
		else if (response.ReceivedSymbol.Key == 'A')
		{
			_Animatior.SetTrigger("A");
		}
		else if (response.ReceivedSymbol.Key == 'A')
		{
			_Animatior.SetTrigger("A");
		}
		else if (response.ReceivedSymbol.Key == 'S')
		{
			_Animatior.SetTrigger("S");
		}
		else if (response.ReceivedSymbol.Key == 'D')
		{
			_Animatior.SetTrigger("D");
		}
		else if (response.ReceivedSymbol.Key == 'Q')
		{
			_Animatior.SetTrigger("Q");
		}
		else if (response.ReceivedSymbol.Key == 'E')
		{
			_Animatior.SetTrigger("E");
		}
	}

	public void OnSymbolSingalManagerClear()
	{

	}

	// Start is called before the first frame update
	void Start()
	{
		_Animatior = GetComponent<Animator>();
		GameManager.GetInstance().LevelManager.SymbolSignalManager.Subscribe(this);
	}

	// Update is called once per frame
	void Update()
	{ }
}
