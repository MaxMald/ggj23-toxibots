﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public enum GameManagerState
	{
		kIdle,
		kInitNextLevel,		
		kStartLevel,
		kUpdateLevel,
		kEvaluateLevels,
		kGameCompleted,
		kGameFailure,
		kInDelayedTranstion,
	}
}
