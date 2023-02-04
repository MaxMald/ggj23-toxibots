using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	/// <summary>
	/// Represent a single keyboard symbol.
	/// </summary>
	public class Symbol
	{
		/// <summary>
		/// The key that represents this symbol
		/// </summary>
		public Char Key { get; private set; }

		/// <summary>
		/// Instantiates a new Symbol.
		/// </summary>
		/// 
		/// <param name="key">The key that represents this symbol.</param>
		public Symbol(Char key)
		{
			Key = key;
		}
	}
}
