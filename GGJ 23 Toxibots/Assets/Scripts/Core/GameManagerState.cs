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
