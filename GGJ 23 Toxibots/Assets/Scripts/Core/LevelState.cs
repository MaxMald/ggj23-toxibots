namespace Assets.Scripts
{
	public enum LevelState
	{
		kIdle,
		kGenerateNewSequence,
		kPresentSequence,
		kReceiveSequence,
		kEvaluateSequences,
		kLevelFailure,
		kLevelCompleted
	}
}
