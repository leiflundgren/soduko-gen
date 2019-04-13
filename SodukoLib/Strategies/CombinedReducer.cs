using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodukoLib.Strategies
{
    public class CombinedReducer : IReducer
    {
        private List<IReducer> reducers;

        public CombinedReducer(params IReducer[] reducers)
            : this((ICollection<IReducer>)reducers)
        {}

        public CombinedReducer(ICollection<IReducer> reducers)
        {
            this.reducers = new List<IReducer>(reducers);
        }

        public bool CanBeRemoved(Board b, Coord c)
        {
            return reducers.Any(r => r.CanBeRemoved(b, c));
        }

        public string Name { get { return nameof(CombinedReducer) + ":[ " + string.Join(", ", reducers.ConvertAll(r => r.Name)) + " ]"; } }
    }
}
