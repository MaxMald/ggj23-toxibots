using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public interface ISymbolSignalManagerListener
	{
		/// <summary>
		/// Called when the symbol signal manager has been initialized.
		/// </summary>
		/// 
		/// <param name="expectedSymbols">The list of expected symbols.</param>
		public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols);

		/// <summary>
		/// Called when a new symbol signal has been received.
		/// </summary>
		/// 
		/// <param name="response">Symbol response.</param>
		public void OnSymbolSignalReceived(SymbolResponse response);

		/// <summary>
		/// Called when the new symbol signal has been cleared.
		/// </summary>
		public void OnSymbolSingalManagerClear();
	}
}
