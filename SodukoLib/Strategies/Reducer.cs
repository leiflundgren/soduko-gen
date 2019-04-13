using System.Linq;
using System.Text;

namespace SodukoLib
{
    public interface IReducer
    {
        bool CanBeRemoved(Board b, Coord c);
        string Name { get; }
    }
}
