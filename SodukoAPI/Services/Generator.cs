using SodukoAPI.Model;
using SodukoLib;

namespace SodukoAPI.Services
{
    public class Generator
    {
        public SodukoBoard Generate(string[] reducers, int? percentExtra)
        {
            Board board = SodukoLib.Generator.Generate();

            ReducerEngine reducerEngine = new ReducerEngine(board, CreateReducer(reducers));

            SodukoBoard webBoard = Convert(board, reducerEngine.CurrentBoard);

            return webBoard;
        }


        private static IReducer CreateReducer(string reducer_name)
        {
            if (string.Equals(reducer_name, "OnlyOnePossibleReducer", StringComparison.OrdinalIgnoreCase)
                || string.Equals(reducer_name, "OnlyPossible", StringComparison.OrdinalIgnoreCase))
                return new SodukoLib.Strategies.OnlyOnePossibleReducer();
            if (string.Equals(reducer_name, "PidgeonHolePrinciple", StringComparison.OrdinalIgnoreCase)
                || string.Equals(reducer_name, "pidgeonHole", StringComparison.OrdinalIgnoreCase))
                return new SodukoLib.Strategies.PidgeonHolePrinciple();

            if (string.Equals(reducer_name, "CombinedReducer", StringComparison.OrdinalIgnoreCase)
                || string.Equals(reducer_name, "combined", StringComparison.OrdinalIgnoreCase))
                return
                    new SodukoLib.Strategies.CombinedReducer(
                        new IReducer[] { new SodukoLib.Strategies.OnlyOnePossibleReducer(),
                            new SodukoLib.Strategies.PidgeonHolePrinciple(),
                        });

            throw new ArgumentOutOfRangeException(nameof(reducer_name), $"Reducer '{reducer_name}' is unknown");
        }

        private static IReducer CreateReducer(string[] reducer_names)
        {
            if (reducer_names.Length == 0) throw new ArgumentException(nameof(reducer_names), "empty array");

            if (reducer_names.Length == 1)
                return CreateReducer(reducer_names[0]);

            IReducer[] reducers = Array.ConvertAll(reducer_names, name => CreateReducer(name));
            return new SodukoLib.Strategies.CombinedReducer(reducers);
        }

        private static SodukoBoard Convert(Board complete, Board reduced)
        {
            SodukoBoard.SodukoField ConvertField(Coord coord, Board complete, Board reduced)
            {
                int value = complete[coord];
                int reduced_value = reduced[coord];

                char chr = value.ToString(System.Globalization.CultureInfo.InvariantCulture)[0];
                bool visible = (reduced_value > 0);
                return new SodukoBoard.SodukoField(chr, visible);
            }

            SodukoBoard board = new SodukoBoard(complete.Rows.Count);
            foreach (NineGroup row in complete.Rows)
            {
                foreach (Coord coord in complete.Coordinates)
                {
                    var field = ConvertField(coord, complete, reduced);
                    board.Board[coord.y][coord.x] = field;
                }
            }

            return board;
        }

    }
}