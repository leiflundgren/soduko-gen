namespace SodukoAPI.Model
{
    public class SodukoBoard
    {
        public static int SODUKU_SIZE = 9;
        public struct SodukoField
        {
            public char Value;
            public bool Visible;

            public SodukoField() : this('\0', false) { }
            public SodukoField(char value, bool visible)
            {
                Value = value;
                Visible = visible;
            }
        }
        public SodukoField[][] Board = GenerateEmptyBoard(SODUKU_SIZE);

        public static SodukoField[][] GenerateEmptyBoard(int size)
        {
            SodukoField[][] board = new SodukoField[size][];
            for ( int y=0; y<size; y++ )
            {
                board[y] = new SodukoField[size];
                for(int x=0; x<size; ++x )
                    board[y][x] = new SodukoField();
            }
            return board;
        }
    }
}
