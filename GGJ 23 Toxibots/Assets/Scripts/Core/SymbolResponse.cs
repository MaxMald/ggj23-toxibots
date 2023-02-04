using System;

namespace Assets.Scripts
{
	public class SymbolResponse
	{
		/// <summary>
		/// Expected symbol.
		/// </summary>
		public Symbol ExpectedSymbol { get; private set; }

		/// <summary>
		/// Received symbol from the player.
		/// </summary>
		public Symbol ReceivedSymbol { get; private set; }

		/// <summary>
		/// Indicates if the received response is correct.
		/// </summary>
		public Boolean IsCorrect { get; private set; }

		/// <summary>
		/// Instantiates a new symbol response.
		/// </summary>
		/// 
		/// <param name="expectedSymbol"></param>
		/// <param name="receivedSymbol"></param>
		/// <param name="isCorrect"></param>
		public SymbolResponse(
			Symbol expectedSymbol, 
			Symbol receivedSymbol, 
			Boolean isCorrect)
		{
			ExpectedSymbol = expectedSymbol;
			ReceivedSymbol = receivedSymbol;
			IsCorrect = isCorrect;
		}
	}
}
