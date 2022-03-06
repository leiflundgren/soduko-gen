using Microsoft.AspNetCore.Mvc;
using SodukoAPI.Model;
using SodukoAPI.Services;

namespace SodukoAPI.Controllers
{
    public class HomeController : Controller
    {
        private Generator generator;

        public HomeController(Services.Generator generator)
        {
            this.generator = generator;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<SodukoBoard> GenerateObject(string reducers = "", int? percentExtra = 30)
        {
            Model.SodukoBoard board = GenerateInternal(reducers, percentExtra);

            return Ok(board);
        }

        [HttpGet]
        // [Produces("application/json")]
        public string Generate(string reducers = "", int? percentExtra = 30)
        {
            Model.SodukoBoard board = GenerateInternal(reducers, percentExtra);

            string jsonString = JSonSerilize(board);
            return jsonString;

            // return $"reducers [{string.Join(", ", reducers)}] extra={percentExtra}";
        }


        private SodukoBoard GenerateInternal(string reducers = "", int? percentExtra = 30)
        {
            string[] reducers_array = reducers.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Model.SodukoBoard board = generator.Generate(reducers_array, percentExtra.GetValueOrDefault(30));
            return board;
        }


        public static string JSonSerilize(SodukoBoard board)
        {

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Formatting = Newtonsoft.Json.Formatting.None;

            using (TextWriter textwriter = new StringWriter())
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(textwriter))
            {
                serializer.Serialize(writer, board);
                return textwriter.ToString() ?? "<null>";
            }
        }

        //private Model.SodukoBoard Convert(SodukoLib.ReducerEngine reducerEngine)
        //{ 
        //    Model.SodukoBoard board = generator.Generate
        //    for(int row = 0; row < reducerEngine.CompleteBoard.Rows.Count; row++)
        //    { 
        //        NineGroup grp_complete = reducerEngine.CompleteBoard.Rows[row];
        //        NineGroup grp_reduced = reducerEngine.CurrentBoard.Rows[row];
        //    for(int column = 0; column < reducerEngine.CompleteBoard.Rows.Count; row++)
        //}

        //private static IReducer CreateReducer(string reducer_name)
        //{
        //    if (string.Equals(reducer_name, "OnlyOnePossibleReducer", StringComparison.OrdinalIgnoreCase)
        //        || string.Equals(reducer_name, "OnlyPossible", StringComparison.OrdinalIgnoreCase))
        //        return new SodukoLib.Strategies.OnlyOnePossibleReducer();
        //    if (string.Equals(reducer_name, "PidgeonHolePrinciple", StringComparison.OrdinalIgnoreCase)
        //    || string.Equals(reducer_name, "pidgeonHole", StringComparison.OrdinalIgnoreCase))
        //        return new SodukoLib.Strategies.PidgeonHolePrinciple();

        //    if (string.Equals(reducer_name, "CombinedReducer", StringComparison.OrdinalIgnoreCase)
        //    |   | string.Equals(reducer_name, "combined", StringComparison.OrdinalIgnoreCase))
        //        return 
        //            new SodukoLib.Strategies.CombinedReducer(
        //                new IReducer[] { new SodukoLib.Strategies.OnlyOnePossibleReducer(), 
        //                    new SodukoLib.Strategies.PidgeonHolePrinciple(), 
        //                });

        //    throw new ArgumentOutOfRangeException(nameof(reducer_name), $"Reducer '{reducer_name}' is unknown");
        //}

        //private static IReducer CreateReducer(string[] reducer_names)
        //{
        //    if (reducer_names.Length == 0) throw new ArgumentException(nameof(reducer_names), "empty array");

        //    if ( reducer_names.Length == 1)
        //        return CreateReducer(reducer_names[0]);


        //        IReducer[] reducers = Array.ConvertAll(reducer_names, name => CreateReducer(name))
        //        return new SodukoLib.Strategies.CombinedReducer(reducers);

        //}
    }
}
