using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib.Strategies
{
    public class CombinedReducer : ReducerBase
    {
        private List<IReducer> reducers;

        public CombinedReducer(params IReducer[] reducers)
            : this((ICollection<IReducer>)reducers)
        {}

        public CombinedReducer(ICollection<IReducer> reducers)
            : base(nameof(CombinedReducer))
        {
            this.reducers = new List<IReducer>(reducers);
        }

        public override bool CanBeRemoved(Board b, Coord c)
        {
            return reducers.Any(r => r.CanBeRemoved(b, c));
        }
        public override string Name => $"{nameof(CombinedReducer)} [{string.Join(", ", reducers.ConvertAll(r => r.Name))}]";

    }
}
