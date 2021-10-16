using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Components
{
    public static class CollectionExtensions
    {
        public static bool ArraySameElements<T>(T[] t1, T[] t2)
        {
            for (int i = 0; i < t1.Length; ++i)
            {
                bool found = false;
                for (int j = i + 1; j < t2.Length; ++j)
                    if (Equals(t1[i], t2[j]))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    return false;
            }
            return true;
        }
    }
}
