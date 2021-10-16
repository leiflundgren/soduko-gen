using SodukoLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Strategies
{
    public class NakedPairPrinciple : IReducer
    {
        public string Name => nameof(NakedPairPrinciple);
        public int SiblingCount { get; private set; }

        private IReducer inner;

        public NakedPairPrinciple(int siblingCount, IReducer reducer)
        {
            SiblingCount = siblingCount;
            inner = reducer;
        }

        /// <summary>
        /// Returns a list of N siblings, including the coord
        /// Or null, if nothing
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public ICollection<Coord> GetSiblings(Board b, Coord c)
        {
            int[] possiblesOfC = b.GetPossibles(c).ToArray();
            if (possiblesOfC.Length != SiblingCount)
                return null;

            List<Coord> siblings = new List<Coord>(SiblingCount);
            siblings.Add(c);

            foreach (NineGroup nine in b.AllGroups)
            {
                foreach (Coord c2 in nine.GetCoordsExcept(c))
                {
                    int[] possC2 = b.GetPossibles(c2).ToArray();
                    if (possC2.Length == SiblingCount && CollectionExtensions.ArraySameElements(possiblesOfC, possC2))
                    {
                        siblings.Add(c2);
                        if (siblings.Count == SiblingCount)
                            return siblings;
                    }
                }
            }

            return null;
        }

        public bool CanBeRemoved(Board b, Coord c)
        {
            ICollection<Coord> siblings = GetSiblings(b, c);
            if (siblings == null)
                return false;
            Board b2 = new Board(b);
            foreach (Coord c2 in siblings)
                b2[c] = -1; // mark as known, but not any of 1-9

            return inner.CanBeRemoved()
        }
    }
}
 