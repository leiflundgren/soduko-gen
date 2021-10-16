using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SodukoLib;
using SodukoLib.Strategies;

namespace SodukoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime t0 = DateTime.UtcNow;

            //for ( int i=0; i<1000; i++ )
            //    Board.Generate();

            //DateTime t1 = DateTime.UtcNow;

            //Console.Out.WriteLine("Generated 1000 boards in " + (t1-t0).TotalMilliseconds);
            Stopwatch w = new Stopwatch();
            Board board = Board.Generate();

            foreach ( IReducer reducer in new IReducer[] {
                new OnlyOnePossibleReducer(),
                new PidgeonHolePrinciple(),
                new CombinedReducer(new OnlyOnePossibleReducer(), new PidgeonHolePrinciple())
            } )
            {
                try
                {
                    ReducerEngine re = new ReducerEngine(board, new OnlyOnePossibleReducer());
                    TimeSpan dt = w.Elapsed;

                    Console.Out.WriteLine("Took board");
                    Console.Out.WriteLine(re.CompleteBoard);
                    Console.Out.WriteLine();
                    Console.Out.WriteLine("Reduced to");
                    Console.Out.WriteLine(re.CurrentBoard);
                    Console.Out.WriteLine("in " + re.History.Count + " moves, using " + reducer.Name + " in " + dt.TotalMilliseconds.ToString("G") + " ms.");
                    Console.Out.WriteLine("==================");
                }
                catch ( ReducerEngine.ReduceFailedException ex )
                {
                    Console.Out.WriteLine("Failed to reduce " + ex.Board);
                }
            }
        }
    }
}