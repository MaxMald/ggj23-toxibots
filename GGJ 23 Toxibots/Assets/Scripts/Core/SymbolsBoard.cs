using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
	public class SymbolsBoard
	{
		/// <summary>
		/// The list of symbol sequence.
		/// </summary>
		private List<Symbol> _SymbolSequence;

		/// <summary>
		/// The list of symbols presented in this board.
		/// </summary>
		private List<Symbol> _VisibleSymbols;

		/// <summary>
		/// The list of available symbols;
		/// </summary>
		private List<Symbol> _AvailableSymbols;

		/// <summary>
		/// The list of sequences and its size;
		/// </summary>
		private Queue<Int32> _SequencesSizes;

		/// <summary>
		/// Symbols' bag.
		/// </summary>
		private Queue<Symbol> _SymbolsBag;

		/// <summary>
		/// The time in between symbols at the time of presentation on the board. 
		/// </summary>
		private float _SymbolStepDuration;

		/// <summary>
		/// The elapsed time.
		/// </summary>
		private float _ElapsedTime;

		/// <summary>
		/// Indicates if the SymbolsBoard has completed its current sequence.
		/// </summary>
		private Boolean _IsCompleted;

		/// <summary>
		/// Observers.
		/// </summary>
		private List<ISymbolsBoardListener> _Subscribers;

		/// <summary>
		/// Random.
		/// </summary>
		private Random _Random;

		/// <summary>
		/// The list of symbols presented in this board.
		/// </summary>
		public IReadOnlyList<Symbol> VisibleSymbols => _VisibleSymbols;

		/// <summary>
		/// The list of symbol sequence.
		/// </summary>
		public IReadOnlyList<Symbol> SymbolSequence => _SymbolSequence;

		/// <summary>
		/// Indicates if the SymbolsBoard has completed its sequence.
		/// </summary>
		public Boolean IsCompleted => _IsCompleted;

		/// <summary>
		/// Instantiates a new SymbolsBoard.
		/// </summary>
		public SymbolsBoard()
		{
			_SymbolSequence = new List<Symbol>();
			_Subscribers = new List<ISymbolsBoardListener>();
			_VisibleSymbols = new List<Symbol>();
			_SymbolsBag = new Queue<Symbol>();
			_SequencesSizes = new Queue<Int32>();
			_AvailableSymbols = new List<Symbol>();
			_Random = new Random();
			_IsCompleted = true;
		}

		/// <summary>
		/// Set the SymbolBoard to its initial values.
		/// </summary>
		/// <param name="symbols"></param>
		/// <param name="symbolStepDuration"></param>
		public void Init(
			List<Symbol> symbols, 
			float symbolStepDuration,
			Int32[] sequencesSizes)
		{
			Clear();
			_SymbolStepDuration = symbolStepDuration;
			_ElapsedTime = 0;
			_IsCompleted = false;
			_AvailableSymbols = symbols.ToList();
			_SymbolsBag = new Queue<Symbol>();
			_SequencesSizes = new Queue<Int32>(sequencesSizes);
			
		}

		public void GenerateNextSequence()
		{
			Clear();
			_IsCompleted = false;
			_ElapsedTime = 0;			
			Int32 sequenceSize = _SequencesSizes.Dequeue();
			for(Int32 i = 0; i < sequenceSize; ++i)
			{
				_SymbolSequence.Add(_AvailableSymbols[_Random.Next(_AvailableSymbols.Count)]);
			}
			_SymbolsBag = new Queue<Symbol>(_SymbolSequence);
		}

		/// <summary>
		/// Updates the SymbolBoard.
		/// </summary>
		/// 
		/// <param name="deltaTime">Delta time.</param>
		public void Update(float deltaTime)
		{
			if (_IsCompleted) return;

			_ElapsedTime += deltaTime;
			if (_ElapsedTime >= _SymbolStepDuration)
			{
				_ElapsedTime -= _SymbolStepDuration;
				if (_SymbolsBag.Count == 0)
				{
					_IsCompleted = true;
					foreach (ISymbolsBoardListener listener in _Subscribers)
					{
						listener.OnSymbolsBoardSequenceCompleted();
					}
					return;
				}

				Symbol newSymbol = _SymbolsBag.Dequeue();
				_VisibleSymbols.Add(newSymbol);
				foreach (ISymbolsBoardListener listener in _Subscribers)
				{
					listener.OnSymbolPresented(newSymbol, VisibleSymbols.ToList());
				}
			}
		}

		public void Subscribe(ISymbolsBoardListener listener)
		{
			_Subscribers.Add(listener);
		}

		/// <summary>
		/// Indicates if the SymbolsBoard has sequences.
		/// </summary>
		/// <returns></returns>
		public Boolean HasSequences()
		{
			return _SequencesSizes.Count > 0;
		}

		private void Clear()
		{
			_SymbolSequence.Clear();
			_VisibleSymbols.Clear();
			foreach (ISymbolsBoardListener listener in _Subscribers)
			{
				listener.OnSymbolBoardCleared();
			}
		}
	}
}
