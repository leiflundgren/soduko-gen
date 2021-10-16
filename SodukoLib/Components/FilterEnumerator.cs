using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Components
{
    public class FilterEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private IEnumerator<T> inner;
        private Predicate<T> predicate;

        public FilterEnumerator(IEnumerator<T> inner, Predicate<T> predicate)
        {
            this.inner = inner;
            this.predicate = predicate;
        }
        public FilterEnumerator(IEnumerable<T> inner, Predicate<T> predicate)
            : this(inner.GetEnumerator(), predicate)
        {}
       
            
        #region IEnumerator

        public T Current => inner.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            inner.Dispose();
        }
        public bool MoveNext()
        {
            for (; ; )
            {
                if (!MoveNext())
                    return false;
                if (predicate(Current))
                    return true;
            }
        }

        public void Reset()
        {
            inner.Reset();
        }
        #endregion


        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

    }
}
