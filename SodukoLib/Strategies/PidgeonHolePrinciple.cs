using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib.Strategies
{
    public class PidgeonHolePrinciple : IReducer
    {
        public bool CanBeRemoved(Board b, Coord c)
        {
            int n = b[c];
            Board b2 = new Board(b);
            b2[c] = 0;

            String sb1 = b.ToString();
            String sb2 = b2.ToString();

            ICollection<Coord> possiblesOfInt = b2.GetPossibles(n);

            return possiblesOfInt.Count == 1;
        }
        public string Name => nameof(PidgeonHolePrinciple);
    }
}
