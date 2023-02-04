namespace Assets.Scripts
{
	/// <summary>
	/// Interface get notifications about the events and status of a
	/// LevelManager.
	/// </summary>
	public interface ILevelManagerListener
	{
		/// <summary>
		/// Called when the level has changed.
		/// </summary>
		/// 
		/// <param name="levelName">The name of the new level.</param>
		public void OnLevelChanged(string levelName);

		/// <summary>
		/// Called when the level has started.
		/// </summary>
		/// 
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelStarted(string levelName);

		/// <summary>
		/// Called when the level has been reset.
		/// </summary>
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelReset(string levelName);		

		/// <summary>
		/// Called if the current level has reached the winning condition.
		/// </summary>
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelCompleted(string levelName);

		/// <summary>
		/// Called if the sequence has been completed.
		/// </summary>
		/// <param name="levelName"></param>
		public void OnLevelSequenceCompleted(string levelName);

		/// <summary>
		/// Called if the current level has reached the fail condition.
		/// </summary>
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelFail(string levelName);

		/// <summary>
		/// Called if the current level has been paused.
		/// </summary>
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelPaused(string levelName);

		/// <summary>
		/// Called if the current level has been resumed. 
		/// </summary>
		/// <param name="levelName">The name of the current level.</param>
		public void OnLevelResumed(string levelName);

		/// <summary>
		/// Called when the LevelManager's state has changed.
		/// </summary>
		/// 
		/// <param name="state">The new LevelState.</param>
		public void OnLevelManagerStateChanged(LevelState state);
	}
}