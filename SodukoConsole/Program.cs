using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SodukoLib;

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

            ReducerEngine re = new ReducerEngine(Board.Generate(), new OnlyOnePossibleReducer());
            Console.Out.WriteLine("Took board");
            Console.Out.WriteLine(re.CompleteBoard);
            Console.Out.WriteLine();
            Console.Out.WriteLine("Reduced to");
            Console.Out.WriteLine(re.CurrentBoard);
            Console.Out.WriteLine("in " + re.History.Count + " moves.");
           
        }

    }
}
;