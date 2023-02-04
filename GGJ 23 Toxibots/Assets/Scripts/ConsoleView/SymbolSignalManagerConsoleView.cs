using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ConsoleView
{
	internal class SymbolSignalManagerConsoleView : ISymbolSignalManagerListener
	{
		private bool show;

		public SymbolSignalManagerConsoleView(bool show)
		{
			this.show = show;
		}

		public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
		{ }

		public void OnSymbolSignalReceived(SymbolResponse response)
		{
			if (!show)
				return;

			Debug.LogFormat(
				"SYMBOL SIGNAL MANAGER: ( received: {0}, expected: {1}, result: {2}",
				response.ReceivedSymbol.Key.ToString(),
				response.ExpectedSymbol.Key.ToString(),
				( response.IsCorrect ? "CORRECT" : "INCORRECT" )
			);
		}

		public void OnSymbolSingalManagerClear()
		{
		}
	}
}
