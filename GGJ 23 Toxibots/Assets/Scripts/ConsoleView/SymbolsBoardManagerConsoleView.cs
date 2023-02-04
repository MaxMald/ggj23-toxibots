using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ConsoleView
{
	internal class SymbolsBoardManagerConsoleView : ISymbolsBoardListener
	{
		public bool show;

		public SymbolsBoardManagerConsoleView(bool show)
		{
			this.show = show;
		}

		public void OnSymbolBoardCleared()
		{ }

		public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols)
		{
			if (!show)
				return;

			String msg = "SYMBOL BOARD MANAGER: Presenting Sequence: ";
			foreach (Symbol symbol in visibleSymbols)
			{
				msg += symbol.Key.ToString() + " ";
			}
			Debug.LogFormat(msg);
		}

		public void OnSymbolsBoardSequenceCompleted()
		{
			if (!show)
				return;

			Debug.LogFormat("SYMBOL BOARD MANAGER: -- Sequence Presentation Completed --");
		}
	}
}
