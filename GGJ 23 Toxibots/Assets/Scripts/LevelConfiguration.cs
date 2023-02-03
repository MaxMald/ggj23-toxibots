using System;

namespace Assets.Scripts
{
	public class LevelConfiguration
	{
		/// <summary>
		/// Level's name.
		/// </summary>
		public String name;

		/// <summary>
		/// The initial value of the meter.
		/// </summary>
		public Int32 meter_initial_value;

		/// <summary>
		/// The filling speed of the meter (units per second).
		/// </summary>
		public Int32 meter_filling_speed;

		/// <summary>
		/// List of available symbols in this level.
		/// </summary>
		public Char[] symbols;

		/// <summary>
		/// The number of sequences and their sizes.
		/// </summary>
		public Int32[] sequences_size;
	}
}
