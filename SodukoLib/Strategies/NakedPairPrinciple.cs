using SodukoLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Strategies
{
    public class NakedPairPrinciple : ReducerBase
    {
        public int SiblingCount { get; private set; }

        private IReducer inner;
        private Dictionary<Board, Board> pair_replaced_cache;

        public NakedPairPrinciple(int siblingCount, IReducer reducer)
            : base(nameof(NakedPairPrinciple))
        {
            SiblingCount = siblingCount;
            inner = reducer;
            pair_replaced_cache = new Dictionary<Board, Board>(Board.EqualsForSsetCoords);
        }

        public override void Clear()
        {
            pair_replaced_cache = new Dictionary<Board, Board>(Board.EqualsForSsetCoords);
            base.Clear();
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
                    if (possC2.Length == SiblingCount && Components.CollectionExtensions.ArraySameElements(possiblesOfC, possC2))
                    {
                        siblings.Add(c2);
                        if (siblings.Count == SiblingCount)
                            return siblings;
                    }
                }
            }

            return null;
        }

        public override bool CanBeRemoved(Board b, Coord c)
        {
            Board b2;
            if ( !pair_replaced_cache.TryGetValue(b, out b2))
            {
                b2 = new Board(b);
                FindAllPairs(b2);
                pair_replaced_cache[b] = b2;
            }

            return inner.CanBeRemoved(b2, c);
        }

        private void FindAllPairs(Board b)
        {
            LinkedList<Coord> coords = new LinkedList<Coord>(Board.allCoords);
            while (coords.First != null)
            {
                Coord c = coords.First.Value;
                coords.RemoveFirst();

                ICollection<Coord> siblings = GetSiblings(b, c);
                if (siblings != null)
                {
                    //Console.Out.WriteLine($"Found that we have siblings [{}")

                    foreach (Coord s in siblings)
                    {
                        coords.Remove(s);
                        b[s] = -1; // mark as known, but not any of 1-9
                    }
                }
            }
        }
    }
}
 