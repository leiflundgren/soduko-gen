using NUnit.Framework;
using SodukoAPI.Controllers;
using SodukoAPI.Model;
using System.IO;

namespace UnitTests
{
    public class TestJSon
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_System_Text_Json()
        {
            SodukoBoard board = GenerateBoard();

            string json_baord = HomeController.JSonSerilize(board);

            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string json_fields = System.Text.Json.JsonSerializer.Serialize(board.Board, options); 

        }

        [Test]
        public void Test_Newtonsoft_Json()
        {
            SodukoBoard board = GenerateBoard();
            string json_baord, json_fields;



            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (TextWriter textwriter = new StringWriter())
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(textwriter))
            {
                serializer.Serialize(writer, board);
                json_baord = textwriter.ToString() ?? "<null>";
            }

            using (TextWriter textwriter = new StringWriter())
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(textwriter))
            {
                serializer.Serialize(writer, board.Board);
                json_fields = textwriter.ToString() ?? "<null>";
            }

        }




        private static SodukoBoard GenerateBoard()
        {
            SodukoBoard board = new SodukoBoard(9);
            for (int x = 0; x < 9; ++x)
                for (int y = 0; y < 9; ++y)
                    board.Board[x][y] = new SodukoBoard.SodukoField('0', true);
            return board;
        }
    }
}