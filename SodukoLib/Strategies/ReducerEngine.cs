using SodukoLib.Components;
using System;
using System.Collections.Generic;

namespace SodukoLib
{
    public class ReducerEngine
    {
        public Board CompleteBoard { get; private set; }
        private readonly IDictionary<Board, bool> boards = new Dictionary<Board, bool>();
        public ICollection<IReducer> Reducers { get; private set; }
        public ICollection<Coord> History { get; private set; }
        public Board CurrentBoard { get; private set; }

        public ReducerEngine(Board b, params IReducer[] reducers)
        {
            this.CompleteBoard = b;
            this.Reducers = reducers;
            this.History = new LinkedList<Coord>();
            this.CurrentBoard = b;

            Reduce();
        }

        public void Remove(Coord c)
        {
            if (History.Contains(c))
                throw new ArgumentException("Attempt to remove " + c + " which already has been removed!");

            Board old = CurrentBoard;
            CurrentBoard = new Board(CurrentBoard);
            CurrentBoard[c] = 0;
            History.Add(c);
            SetSolvable(CurrentBoard, true);

            Console.Out.WriteLine("Removed " + c);
        }

        public bool IsKnown(Board b, out bool solveable)
        {
            return boards.TryGetValue(b, out solveable);
        }

        public void SetSolvable(Board b, bool isSolvable)
        {
            if (!boards.ContainsKey(b))
                boards.Add(b, isSolvable);
            else
                boards[b] = isSolvable;
        }

        private bool TryReduce(Coord c)
        {
            foreach (IReducer reducer in this.Reducers)
            {
                if (CurrentBoard[c] > 0 && reducer.CanBeRemoved(CurrentBoard, c))
                    return true;
            }
            return false;
        }

        
        private void Reduce()
        {
            Random rnd = new Random();
            int offset = rnd.Next(Board.allCoords.Length);

            bool reduced;
            do
            {
                reduced = false;

                foreach (Coord c in new RandomList<Coord>(Board.allCoords))
                {
                    if (TryReduce(c))
                    {
                        Remove(c);
                        reduced = true;
                        break;
                    }
                }
            }
            while (reduced);
        }


        public class ReduceFailedException : Exception
        {
            public readonly Board Board;
            public readonly ICollection<IReducer> Reducers;

            public ReduceFailedException(string message, Board currentBoard, ICollection<IReducer> reducers) : base(message)
            {
                this.Board = currentBoard;
                this.Reducers = reducers;
            }
        }
    }
}
