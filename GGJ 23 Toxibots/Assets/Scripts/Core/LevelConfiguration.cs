using System;

namespace Assets.Scripts
{
	[Serializable]
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
		/// 
		/// </summary>
		public float symbol_presentation_step_duration;

		/// <summary>
		/// 
		/// </summary>
		public float wrong_symbol_signal_meter_penalty;

		/// <summary>
		/// List of available symbols in this level.
		/// </summary>
		public String[] symbols;

		/// <summary>
		/// The number of sequences and their sizes.
		/// </summary>
		public Int32[] sequences_size;
	}
}
