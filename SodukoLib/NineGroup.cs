using SodukoLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib
{
    public class NineGroup
    {
        protected Board board;
        protected IList<Coord> coords;


        public NineGroup(Board board, params Coord[] coords)
            : this(board, (IList<Coord>)coords)
        {}

        public NineGroup(Board board, IList<Coord> coords)
        {
            if (board == null)
                throw new ArgumentNullException("board");
            if (coords == null)
                throw new ArgumentNullException("coords");

            this.board = board;
            this.coords = coords;
        }

        public NineGroup(Board board, NineGroup other)
            : this(board, other.Coords)
        {}


        public int this[int n]
        {
            get { return this.board[this.coords[n]]; }
            set { this.board[this.coords[n]] = value; }
        }

        public bool Contains(Coord coordinate)
        {
            return ((ICollection<Coord>)coords).Contains(coordinate);
        }

        public Coord Find(int n)
        {
            foreach (Coord c in coords)
                if (board[c] == n)
                    return c;
            return null;
        }

        public bool Contains(int n)
        {
            foreach (Coord c in coords)
                if (board[c] == n)
                    return true;
            return false;
        }


        public override bool Equals(object obj)
        {
            NineGroup other = obj as NineGroup;
            if (other == null)
                return false;

            foreach (Coord c in coords)
                if (!other.Coords.Contains(c))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            int n = 0;
            int i = 0;
            foreach (Coord c in coords)
            {
                int p = Primes.PRIMES[++i];
                n += p * c.GetHashCode();
            }
            return n;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Group:");
            foreach (Coord c in coords)
            {
                sb.Append(c); sb.Append(' ');
            }
            return sb.ToString();
        }

        public IList<Coord> Coords { get { return coords; } }

        public IEnumerable<Coord> GetCoordsExcept(Coord c)
        {
            return new FilterEnumerator<Coord>(Coords, (c_) => Equals(c, c_));
        }
    }
}
