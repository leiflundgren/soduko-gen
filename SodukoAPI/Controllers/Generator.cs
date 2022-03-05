using Microsoft.AspNetCore.Mvc;
using SodukoLib;
using SodukoLib.Strategies;

namespace SodukoAPI.Controllers
{
    [Route("Generate")]

    public class Generate : RouteBase
    {
        public Generate(string? template, string? name, IInlineConstraintResolver constraintResolver, RouteValueDictionary? defaults, IDictionary<string, object>? constraints, RouteValueDictionary? dataTokens) 
            : base(template, name, constraintResolver, defaults, constraints, dataTokens)
        {
        }
        


        // GET api/<GenerateController>/5
        [HttpGet("{Strategy}")]
        public string Get(string strategy, int percentage)
        {
            IReducer reducer;
            if (strategy == nameof(OnlyOnePossibleReducer))
                reducer = new OnlyOnePossibleReducer();
            else if (strategy == nameof(PidgeonHolePrinciple))
                    reducer = new PidgeonHolePrinciple();
            else if (strategy == nameof(CombinedReducer))
                reducer = new CombinedReducer(new OnlyOnePossibleReducer(), new PidgeonHolePrinciple());
            else
                throw new ArgumentOutOfRangeException($"unknown strategy {strategy??"<null>"}");

            Board board = Board.Generate();
            ReducerEngine re = new ReducerEngine(board, new OnlyOnePossibleReducer());
            return re.CompleteBoard.ToString();
        }


        protected override Task OnRouteMatched(RouteContext context)
        {
            return Task.FromResult(0);
        }

        protected override VirtualPathData? OnVirtualPathGenerated(VirtualPathContext context)
        {
            return null;
        }
    }
}
