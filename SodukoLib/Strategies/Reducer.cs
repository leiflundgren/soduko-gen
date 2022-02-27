using System;
using System.Text;

namespace SodukoLib
{
    public interface IReducer : IDisposable
    {
        bool CanBeRemoved(Board b, Coord c);
        string Name { get; }


        /// <summary>
        /// If the reduces stores state beween solutions, clear that
        /// </summary>
        void Clear(); 
    }

    public abstract class ReducerBase : IReducer
    {
        private bool disposedValue;
        private string m_name;

        protected ReducerBase(string name)
        {
            m_name = name;
        }

        public virtual string Name => m_name;

        public abstract bool CanBeRemoved(Board b, Coord c);

        public virtual void Clear()
        {
            // default, do nothing
        }


        #region IDispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ReducerBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
