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
}
