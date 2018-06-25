using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace GameFramework
{
    public class RunTimeWatch : IDisposable
    {
        private Stopwatch m_watch;
        public TimeSpan Elapsed
        {
            get;
            private set;
        }
        public void Dispose()
        {
            m_watch.Stop();
            Elapsed = m_watch.Elapsed;
            m_watch = null;
        }
    }
}
