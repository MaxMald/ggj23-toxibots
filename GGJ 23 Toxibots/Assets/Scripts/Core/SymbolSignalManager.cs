using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class SymbolSignalManager
	{
		private Queue<Symbol> _SymbolsLeft;

		private List<SymbolResponse> _Responses;

		private List<ISymbolSignalManagerListener> _Subscribers;

		public IReadOnlyList<SymbolResponse> Responses => _Responses;

		public Boolean IsCompleted => _SymbolsLeft.Count == 0;

		public SymbolSignalManager()
		{
			_SymbolsLeft = new Queue<Symbol>();
			_Responses = new List<SymbolResponse>();
			_Subscribers = new List<ISymbolSignalManagerListener>();
		}

		public void Init(List<Symbol> expectedSymbolSequence)
		{
			_SymbolsLeft = new Queue<Symbol>(expectedSymbolSequence);
			Clear();
			foreach (ISymbolSignalManagerListener listener in _Subscribers)
			{
				listener.OnSymbolSignalManagerInit(expectedSymbolSequence.ToList());
			}
		}

		public void OnUpdate()
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				SymbolReceived('W');
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				SymbolReceived('A');
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				SymbolReceived('S');
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				SymbolReceived('D');
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				SymbolReceived('Q');
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				SymbolReceived('E');
			}
		}

		public void Subscribe(ISymbolSignalManagerListener listener)
		{
			_Subscribers.Add(listener);
		}

		public void Clear()
		{
			_Responses = new List<SymbolResponse>();
			foreach (ISymbolSignalManagerListener listener in _Subscribers)
			{
				listener.OnSymbolSingalManagerClear();
			}
		}

		private void SymbolReceived(Char symbolChar)
		{
			if (_SymbolsLeft.Count == 0)
			{
				return;
			}

			Symbol expectedSymbol = _SymbolsLeft.Dequeue();
			SymbolResponse response = new SymbolResponse(
				expectedSymbol,
				new Symbol(symbolChar),
				expectedSymbol.Key == symbolChar
			);
			_Responses.Add(response);

			foreach (ISymbolSignalManagerListener listener in _Subscribers)
			{
				listener.OnSymbolSignalReceived(response);
			}
		}
	}
}
