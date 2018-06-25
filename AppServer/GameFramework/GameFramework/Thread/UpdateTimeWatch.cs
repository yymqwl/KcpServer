using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;


namespace GameFramework
{
    public class UpdateTimeWatch : IDisposable
    {
        private Stopwatch m_watch;
        private long m_preTime;
        private long m_ts;
        private long m_internal;// 刷新时间单位

        private bool m_init = false;

        public virtual bool init(long tinternal)
        {
            m_internal = tinternal;
            m_watch = new Stopwatch();
            m_watch.Reset();
            m_watch.Start();
            m_preTime = m_watch.ElapsedMilliseconds;


            m_init = true;
            return m_init;
        }
        public virtual bool Check()//一次刷新
        {
            bool pret = false;
            m_ts = m_watch.ElapsedMilliseconds - m_preTime;
            if (m_ts >= m_internal)
            {
                m_preTime = m_watch.ElapsedMilliseconds;
                pret = true;
            }
            return pret;
        }
        public float Elapsed
        {
            get
            {
                return (float)(m_ts / 1000.0);
            }
        }


        public void Dispose()
        {
            if (m_watch != null)
            {
                m_watch.Stop();
                m_watch = null;
            }
        }
    }
}
