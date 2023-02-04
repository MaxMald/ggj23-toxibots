using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public class Meter : ISymbolSignalManagerListener
	{
		/// <summary>
		/// The meter's max. amount.
		/// </summary>
		public static float METER_MAXIMUM_AMOUNT = 100;

		/// <summary>
		/// Indicates how much the meter is filled.
		/// </summary>
		private float _MeterAmount;

		/// <summary>
		/// 
		/// </summary>
		private float _WrongSignalMeterPenalty;

		/// <summary>
		/// The filling speed of this meter in units per second.
		/// </summary>
		private float _MeterFillingSpeed;

		/// <summary>
		/// The subscribers.
		/// </summary>
		private List<IMeterListener> _Subscibers;

		/// <summary>
		/// Indicates how much the meter is filled.
		/// </summary>
		public float MeterAmount => _MeterAmount;
		
		/// <summary>
		/// 
		/// </summary>
		public Meter()
		{
			_MeterAmount = 0;
			_MeterFillingSpeed = 1;
			_WrongSignalMeterPenalty = 0;
			_Subscibers = new List<IMeterListener>();
		}

		/// <summary>
		/// Initialize the meter values.
		/// </summary>
		/// 
		/// <param name="meterFillingSpeed"></param>
		/// <param name="meterInitialAmount"></param>
		public void Init(
			float meterFillingSpeed, 
			float meterInitialAmount,
			float wrongSignalMeterPenalty)
		{
			_MeterFillingSpeed = meterFillingSpeed;
			_MeterAmount = meterInitialAmount;
			_WrongSignalMeterPenalty = wrongSignalMeterPenalty;
			foreach (IMeterListener listener in _Subscibers)
			{
				listener.OnMeterAmountChanged(_MeterAmount);
			}
		}

		/// <summary>
		/// Increases the meter according to its values.
		/// </summary>
		/// <param name="deltaTime"></param>
		public void Update(float deltaTime)
		{
			float amount = _MeterAmount + deltaTime * _MeterFillingSpeed;
			_MeterAmount = (amount > METER_MAXIMUM_AMOUNT ? METER_MAXIMUM_AMOUNT : amount);
			foreach (IMeterListener listener in _Subscibers) 
			{
				listener.OnMeterAmountChanged(_MeterAmount);
			}
		}

		/// <summary>
		/// Indicates if the meter is filled.
		/// </summary>
		/// 
		/// <returns>true if the meter is filled.</returns>
		public Boolean IsFilled()
		{
			return _MeterAmount >= METER_MAXIMUM_AMOUNT;
		}

		/// <summary>
		/// Add a new subscriber.
		/// </summary>
		/// 
		/// <param name="listener">listener.</param>
		public void Subscribe(IMeterListener listener)
		{
			_Subscibers.Add(listener);
		}

		public void OnSymbolSignalManagerInit(List<Symbol> expectedSymbols)
		{ }

		public void OnSymbolSignalReceived(SymbolResponse response)
		{ 
			if (!response.IsCorrect)
			{
				float amount = _MeterAmount + _WrongSignalMeterPenalty;
				_MeterAmount = (amount > METER_MAXIMUM_AMOUNT ? METER_MAXIMUM_AMOUNT : amount);
				foreach (IMeterListener listener in _Subscibers)
				{
					listener.OnMeterAmountChanged(_MeterAmount);
				}
			}
		}

		public void OnSymbolSingalManagerClear()
		{ }
	}
}
