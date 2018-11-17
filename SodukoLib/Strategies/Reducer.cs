using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib
{
    public interface IReducer
    {
        bool CanBeRemoved(Board b, Coord c);
    }
     

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

        private void Reduce()
        {
            Random rnd = new Random();
            int offset = rnd.Next(Board.allCoords.Length);

            foreach ( IReducer reducer in this.Reducers )
            {
                for (int i = 0, fails = 0; fails < Board.allCoords.Length; i++)
                {
                    int j = (i + offset) % Board.allCoords.Length;
                    Coord c = Board.allCoords[j];
                    if ( CurrentBoard[c] == 0 ||  ! reducer.CanBeRemoved(CurrentBoard, c))
                    {
                        fails++;
                        continue;
                    }

                    fails = 0; // reset
                    Remove(c);
                }
            }
        }
    }
}
