using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GameFramework
{
    public class UpdateTime<T1, T2> : IDisposable
    {

        private float m_ts;
        private float m_elapsedTime;


        private float m_internal;// 刷新时间单位
        private bool m_init = false;


        private event Action< T1, T2> m_evt_act;

        public event Action< T1, T2> Evt_Act
        {
            add
            {
                if (value != null)
                {
                    m_evt_act += value;
                }
            }
            remove
            {
                if (value != null)
                {
                    m_evt_act -= value;
                }
            }



        }

        public UpdateTime()
        {
           
        }
        public virtual bool init(float tinternal)
        {
            m_evt_act = null;
            m_ts = 0;
            m_internal = tinternal;
            m_init = true;
            return m_init;
        }
        public virtual bool Check(float t)//一次刷新
        {
            bool pret = false;
            m_ts += t;
            if (m_ts >= m_internal)
            {
                m_elapsedTime = m_ts;
                m_ts = 0;
                pret = true;
            }
            return pret;
        }

        

        public virtual void OnUpdate(float elapsedTime, T1 t1, T2 t2)
        {
            if (Check(elapsedTime))
            {
                m_evt_act(t1, t2);
            }
        }



        public void Dispose()
        {

        }
    }
}
