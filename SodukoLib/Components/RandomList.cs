using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoLib.Components
{
    public class RandomList<T> : IList<T>
    {

        private IList<T> store;
        private Random random;
        private bool randomize_needed;
        private void RandomizeList()
        {
            if (randomize_needed)
            {// Task last, swap with something earlier.
                for (int i = store.Count-1; i > 0; --i)
                {
                    int j = random.Next(i);

                    T t = store[i];
                    store[i] = store[j];
                    store[j] = t;
                }
                randomize_needed = false;
            }
        }

        public int Count => store.Count;

        public bool IsReadOnly => store.IsReadOnly;

        public T this[int index] 
        { 
            get { RandomizeList(); return store[index]; } 
            set => store[index] = value; 
        }


        public int IndexOf(T item)
        {
            RandomizeList();
            return store.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            store.Insert(index, item);
            randomize_needed = true;
        }

        public void RemoveAt(int index)
        {
            store.RemoveAt(index);
            randomize_needed = true;
        }

        public void Add(T item)
        {
            store.Add(item);
            randomize_needed = true;
        }

        public void Clear()
        {
            store.Clear();
        }

        public bool Contains(T item)
        {
            return store.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            store.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (store.Remove(item))
            {
                randomize_needed = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            RandomizeList();
            return store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public RandomList() : this(new List<T>(), new Random()) { }
        public RandomList(ICollection<T> src) : this(src, new Random()) { }
        public RandomList(ICollection<T> src, Random random) : this(new List<T>(src), random) { }
        public RandomList(List<T> store, Random random)
        {
            this.store = store;
            this.random = random;
            this.randomize_needed = true;
        }

    }
}
