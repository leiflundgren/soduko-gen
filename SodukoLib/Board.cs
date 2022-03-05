using System;
using System.Collections.Generic;
using System.Text;

namespace SodukoLib
{


    public class Board
    {
        private int[] values;
        private NineGroup[] rows;
        private NineGroup[] columns;
        private NineGroup[] squares;
        private NineGroup[] allGroups;

        public static readonly Coord[] allCoords;

        static Board()
        {
            allCoords = new Coord[81];
            for (int x = 0; x < 9; ++x)
                for (int y = 0; y < 9; ++y)
                    allCoords[x + 9 * y] = new Coord(x, y);
        }

        public Board()
        {
            this.values = new int[81];
            for (int i = 0; i < this.values.Length; ++i)
                this.values[i] = 0;


            this.rows = new NineGroup[9];
            for (int r = 0; r < 9; ++r)
            {
                this.rows[r] = new NineGroup(this, new Coord(r, 0), new Coord(r, 1), new Coord(r, 2), new Coord(r, 3), new Coord(r, 4), new Coord(r, 5), new Coord(r, 6), new Coord(r, 7), new Coord(r, 8));
            }

            this.columns = new NineGroup[9];
            for (int c = 0; c < 9; ++c)
            {
                this.columns[c] = new NineGroup(this, new Coord(0, c), new Coord(1, c), new Coord(2, c), new Coord(3, c), new Coord(4, c), new Coord(5, c), new Coord(6, c), new Coord(7, c), new Coord(8, c));
            }

            this.squares = new NineGroup[9];
            for (int x = 0; x < 3; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    int s = x * 3 + y;

                    Coord[] coords = new Coord[9];
                    for (int c0 = 0; c0 < 3; ++c0)
                    {
                        int c = c0 + 3 * x;
                        for (int r0 = 0; r0 < 3; ++r0)
                        {
                            int r = r0 + 3 * y;
                            coords[c0 * 3 + r0] = new Coord(r, c);
                        }
                    }
                    this.squares[s] = new NineGroup(this, coords);
                }
            }

            SetAllGroups();
        }


        public Board(Board other)
        {
            values = new int[other.values.Length];
            rows = new NineGroup[other.rows.Length];
            columns = new NineGroup[other.columns.Length];
            squares = new NineGroup[other.squares.Length];

            Array.Copy(other.values, this.values, other.values.Length);

            for (int i = 0; i < rows.Length; ++i)
            {
                rows[i] = new NineGroup(this, other.rows[i]);
                columns[i] = new NineGroup(this, other.columns[i]);
                squares[i] = new NineGroup(this, other.squares[i]);
            }
            SetAllGroups();
        }
        private void SetAllGroups()
        {
            allGroups = new NineGroup[rows.Length + columns.Length + squares.Length];
            int p = 0;
            foreach (NineGroup g in squares)
                allGroups[p++] = g;
            foreach (NineGroup g in rows)
                allGroups[p++] = g;
            foreach (NineGroup g in columns)
                allGroups[p++] = g;
        }

        public int this[int x, int y]
        {
            get { return this.values[y * 9 + x]; }
            set { this.values[y * 9 + x] = value; }
        }
        public int this[Coord c]
        {
            get { return this[c.x, c.y]; }
            set { this[c.x, c.y] = value; }
        }

        public IList<NineGroup> Squares 
        {
            get { return squares; }
        }
        public IList<NineGroup> Rows
        {
            get { return rows; }
        }
        public IList<NineGroup> Columns
        {
            get { return columns; }
        }
        public ICollection<Coord> Coordinates
        {
            get
            {
                List<Coord> values = new List<Coord>(rows.Length * rows[0].Count);
                foreach (NineGroup grp in rows)
                    values.AddRange(grp.Coords);
                return values;
            }
        }
        public IList<NineGroup> AllGroups => allGroups;

        public List<NineGroup> GetGroupsContaining(Coord c)
        {
            List<NineGroup> res = new List<NineGroup>();
            
            foreach (NineGroup ng in allGroups)
            {
                if (ng.Contains(c) )
                    res.Add(ng);
            }
            return res;
        }

        public bool IsPossible(Coord c, int n)
        {
            foreach (NineGroup ng in allGroups)
            {
                if (!ng.Contains(c))
                    continue;
                Coord c0 = ng.Find(n);
                if ( c0 != null && c0 != c)
                    return false;
            }
            return true;
        }

   

        public IList<int> GetPossibles(Coord c)
        {
            string sb = this.ToString();
            List<int> possibles = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            foreach (NineGroup ng in GetGroupsContaining(c))
            {
                for ( int i=0; i<possibles.Count; i++)
                {
                    int n = possibles[i]; 
                    if ( !ng.Contains(n) )
                        continue;

                    possibles.RemoveAt(i);
                    --i; // compensate for upcoming ++i
                }
            }
            return possibles;
        }


        public ICollection<Coord> GetPossibles(int n)
        {
            List<Coord> possibles = new List<Coord>();

            foreach (Coord c in allCoords)
            {
                bool contained = false;
                foreach (NineGroup ng in GetGroupsContaining(c))
                {
                    if (ng.Contains(n))
                    {
                        contained = true;
                        break;
                    }
                }

                if (!contained)
                    possibles.Add(c);
            }

            return possibles;
        }

        public bool IsValid
        {
            get
            {
                foreach (Coord c in allCoords)
                {
                    int n = this[c];
                    if (n != 0 && !IsPossible(c, n))
                        return false;
                }
                return true;
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y <= 9; ++y)
            {
                if ((y % 3) == 0)
                    sb.AppendLine("-------------");
                if (y < 9)
                {
                    for (int x = 0; x <= 9; x++)
                    {
                        if ((x % 3) == 0)
                            sb.Append('|');
                        if (x < 9)
                        {
                            int n = this[x, y];
                            char c;
                            if ( n == 0)  
                                c = '_';
                            else
                                c = (char)((int)'0' + n);
                            sb.Append(c);
                        }
                    }
                    sb.Append('\n');
                }
            }
            return sb.ToString();
        }

        public static IEqualityComparer<Board> EqualsEverywhere => new EqualsInAll();
        public static IEqualityComparer<Board> EqualsForSsetCoords => new EqualsInSetCoords();


        /// <summary>
        /// Compare two boards, only check squares that have known data (ignore value <= 0)
        /// </summary>
        private class EqualsInSetCoords : IEqualityComparer<Board>
        {
            public bool Equals(Board x, Board y)
            {
                foreach ( Coord c in allCoords )
                {
                    int vx = x[c], vy = y[c];
                    if (vx > 0 && vy > 0 && vx != vy)
                        return false;
                }
                return true;
            }

            public int GetHashCode(Board b)
            {
                int h = 0;
                foreach (Coord c in allCoords)
                {
                    int v = b[c];
                    if (v > 0)
                        h = h * 19 + v * 11;
                }
                return h;
            }
        }
        /// <summary>
        /// Compare two boards, only check squares that have known data (ignore value <= 0)
        /// </summary>
        private class EqualsInAll : IEqualityComparer<Board>
        {
            public bool Equals(Board x, Board y)
            {
                foreach (Coord c in allCoords)
                {
                    int vx = x[c], vy = y[c];
                    if (vx != vy)
                        return false;
                }
                return true;
            }

            public int GetHashCode(Board b)
            {
                int h = 0;
                foreach (Coord c in allCoords)
                {
                    int v = b[c];
                    h = h * 19 + v * 11;
                }
                return h;
            }
        }
    }
}
