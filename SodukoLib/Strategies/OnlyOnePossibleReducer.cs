using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib
{
    public class OnlyOnePossibleReducer : IReducer 
    {
        public bool CanBeRemoved(Board b, Coord c)
        {
            int n = b[c];
            Board b2 = new Board(b);
            b2[c] = 0;

            String sb1 = b.ToString();
            String sb2 = b2.ToString();

            IList<int> possibles = b2.GetPossibles(c);
            return possibles.Count == 1;
        }
    }
}
