namespace SodukoAPI.Model
{
    [System.Diagnostics.DebuggerDisplay("SodukoBoard({Board.Length}")]
    public class SodukoBoard
    {
        public static int SODUKU_SIZE = 9;
        [System.Diagnostics.DebuggerDisplay("SodukoField({ValueIfVisible})")]
        public class SodukoField
        {
            [Newtonsoft.Json.JsonProperty(PropertyName ="val")]
            public char Value;
            [Newtonsoft.Json.JsonProperty(PropertyName = "vis")]
            public bool Visible;

            [Newtonsoft.Json.JsonIgnore]
            public char ValueIfVisible => (Visible ? Value : 'x');

            public SodukoField() : this('\0', false) { }
            public SodukoField(char value, bool visible)
            {
                Value = value;
                Visible = visible;
            }
        }
        public SodukoField[][] Board;

        /// <summary>
        /// Generates a board with initialized arrays, filled with null-values
        /// </summary>
        /// <param name="size">width/height of the square board</param>
        public SodukoBoard(int size)
        {
            Board = new SodukoBoard.SodukoField[size][];
            for (int row = 0; row < size; row++)
            {
                Board[row] = new SodukoBoard.SodukoField[size];
            }
        }
         
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
