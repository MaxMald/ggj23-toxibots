﻿using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	[Serializable]
	public class GameConfiguration
	{
		/// <summary>
		/// List of levels in the game.
		/// </summary>
		public List<LevelConfiguration> levels;
	}
}
