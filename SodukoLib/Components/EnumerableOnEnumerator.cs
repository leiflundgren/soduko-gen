using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Components
{
    public class EnumerableOnEnumerator<T> : IEnumerable<T>
    {
        private IEnumerator<T> enumerator;

        public EnumerableOnEnumerator(IEnumerator<T> en)
        {
            this.enumerator = en;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
