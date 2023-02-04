namespace Assets.Scripts
{
	public interface IMeterListener
	{
		/// <summary>
		/// Called every time the meter's amount has changed.
		/// </summary>
		/// 
		/// <param name="meterAmount">The new amount of the meter.</param>
		public void OnMeterAmountChanged(float meterAmount);
	}
}
