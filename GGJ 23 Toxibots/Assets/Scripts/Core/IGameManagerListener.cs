namespace Assets.Scripts
{
	public interface IGameManagerListener
	{
		/// <summary>
		/// Called when the state of the GameManager has changed.
		/// </summary>
		/// 
		/// <param name="newState">The new GameManagerState.</param>
		public void OnGameManagerStateChanged(GameManagerState newState);

		/// <summary>
		/// Called when the game has started.
		/// </summary>
		public void OnGameStarted();

		/// <summary>
		/// Called when the game has reset.
		/// </summary>
		public void OnGameReset();

		/// <summary>
		/// Called when the game level has changed.
		/// </summary>
		/// 
		/// <param name="levelName">The name of the new level.</param>
		public void OnGameLevelChanged(string levelName);

		/// <summary>
		/// Called when the user has failed the game.
		/// </summary>
		public void OnGameFailed();

		/// <summary>
		/// Called when the user has completed the game.
		/// </summary>
		public void OnGameCompleted();
	}
}
