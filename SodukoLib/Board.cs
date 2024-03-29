﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SodukoLib
{
    public class Coord
    {
        public int x = 0, y = 0;

        public Coord(int x, int y) { this.x = x; this.y=y; }

        public override bool Equals(object obj)
        {
            Coord other = obj as Coord;
            return other != null && this.x == other.x && this.y == other.y;
        }

        public override int GetHashCode()
        {
            return x*3+y*101;
        }
        public override string ToString()
        {
            return string.Format("[{0},{1}]", x, y);
        }
    }

 
    public class Board
    {
        private int[] values;
        private NineGroup[] rows;
        private NineGroup[] columns;
        private NineGroup[] squares;

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
            for( int r =0; r<9; ++r ) {
                this.rows[r] = new NineGroup(this, new Coord(r, 0), new Coord(r, 1), new Coord(r, 2), new Coord(r, 3), new Coord(r, 4), new Coord(r, 5), new Coord(r, 6), new Coord(r, 7), new Coord(r, 8));
            }

            this.columns = new NineGroup[9];
            for( int c =0; c<9; ++c ) {
                this.columns[c] = new NineGroup(this, new Coord(0, c), new Coord(1, c), new Coord(2, c), new Coord(3, c), new Coord(4, c), new Coord(5, c), new Coord(6, c), new Coord(7, c), new Coord(8, c));
            }

            this.squares = new NineGroup[9];
            for ( int x=0; x<3; ++x )  {
                for ( int y=0; y<3; ++y )  {
                    int s = x*3+y;

                    Coord[] coords = new Coord[9];
                    for( int c0 =0; c0<3; ++c0 ) {
                        int c = c0 + 3*x;
                        for ( int r0=0; r0<3; ++r0 ) {
                            int r = r0 + 3*y;
                            coords[c0*3+r0] = new Coord(r, c);
                        }
                    }
                    this.squares[s] = new NineGroup(this, coords);
                }
            }
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

        private List<NineGroup> GetGroupsContaining(Coord c)
        {
            List<NineGroup> res = new List<NineGroup>();
            
            List<NineGroup> groups = new List<NineGroup>();
            groups.AddRange(Squares);
            groups.AddRange(Rows);
            groups.AddRange(Columns);

            foreach (NineGroup ng in groups)
            {
                if (ng.Contains(c) )
                    res.Add(ng);
            }
            return res;
        }

        public bool IsPossible(Coord c, int n)
        {
            List<NineGroup> groups = new List<NineGroup>();
            groups.AddRange(Squares);
            groups.AddRange(Rows);
            groups.AddRange(Columns);

            foreach (NineGroup ng in groups)
            {
                if (!ng.Contains(c))
                    continue;
                Coord c0 = ng.Find(n);
                if ( c0 != null && c0 != c)
                    return false;
            }
            return true;
        }

        public static IList<T> Shuffle<T>(ICollection<T> list0)  
        {  
            Random rng = new Random();  

            IList<T> list = new List<T>(list0);
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
            return list;
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

        public static Board Generate()
        {
            Random rnd = new Random();
            Board b = new Board();

            Populate(b, 0);
            return b;
        }

        private static bool Populate(Board b, int coord_n)
        {
            if ( coord_n == allCoords.Length )
                return true;

            Coord c = allCoords[coord_n];
            IList<int> possibilites = Shuffle( b.GetPossibles(c) );
            if (possibilites.Count == 0)
                return false;

            foreach ( int n in possibilites )
            {
                b[c] = n;
                //Console.Out.WriteLine(c);
                //Console.Out.WriteLine(b);

                if ( Populate(b, coord_n+1))
                    return true;
            }
            b[c] = 0;
            return false;
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

    }
}
