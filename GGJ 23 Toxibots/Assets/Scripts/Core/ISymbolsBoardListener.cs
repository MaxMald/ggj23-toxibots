using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public interface ISymbolsBoardListener
	{
		/// <summary>
		/// Called when a new symbol is presented in the board.
		/// </summary>
		/// <param name="newSymbol"></param>
		/// <param name="visibleSymbols"></param>
		public void OnSymbolPresented(Symbol newSymbol, List<Symbol> visibleSymbols);

		/// <summary>
		/// Called when the symbol board has completed the sequence.
		/// </summary>
		public void OnSymbolsBoardSequenceCompleted();

		/// <summary>
		/// Called when the symbol board has been cleared.
		/// </summary>
		public void OnSymbolBoardCleared();
	}
}
