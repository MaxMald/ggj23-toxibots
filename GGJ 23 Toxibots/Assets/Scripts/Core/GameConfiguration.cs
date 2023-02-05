using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	[Serializable]
	public class GameConfiguration
	{
		/// <summary>
		/// Duration between levels.
		/// </summary>
		public float level_presentation_duration;

		/// <summary>
		/// List of levels in the game.
		/// </summary>
		public List<LevelConfiguration> levels;
	}
}
