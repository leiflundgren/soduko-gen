using System;
using System.Collections.Generic;

namespace SodukoLib
{
    public class Generator
    {

        public static Board Generate()
        {
            Random rnd = new Random();
            Board b = new Board();

            Populate(b, 0);
            return b;
        }

        private static bool Populate(Board b, int coord_n)
        {
            if (coord_n == Board.allCoords.Length)
                return true;

            Coord c = Board.allCoords[coord_n];
            IList<int> possibilites = Components.RandomList<int>.Shuffle(b.GetPossibles(c));
            if (possibilites.Count == 0)
                return false;

            foreach (int n in possibilites)
            {
                b[c] = n;
                //Console.Out.WriteLine(c);
                //Console.Out.WriteLine(b);

                if (Populate(b, coord_n + 1))
                    return true;
            }
            b[c] = 0;
            return false;
        }
    }
}
