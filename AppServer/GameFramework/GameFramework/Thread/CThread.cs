using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using GameFramework;

namespace GameFramework
{
    public class CThread : IDisposable
    {
        private int m_ThreadInternal = 1;



        Thread m_thread=null;
        public Thread Thread
        {
            get
            {
                return m_thread;
            }
            set
            {
                m_thread = value;
            }
        }
        bool m_running = false;
        public bool Running
        {
            get {return m_running; }
            set {m_running=value; }
        }
        
        UpdateTimeWatch m_utw;


        Action<float> m_game_update;
        public Action<float> GameUpdate
        {
            get { return m_game_update; }
            set { m_game_update = value; }
        }

        protected virtual void Run()
        {
            
            while (m_running)
            {
                try
                {
                    if (m_utw.Check())
                    {
                        m_game_update(m_utw.Elapsed);
                    }
                    else
                    {
                        Thread.Sleep(m_ThreadInternal);
                    }
                }
                catch (Exception e)
                {
                    throw new GameFrameworkException("thread Run Exception" + e.ToString());
                }
              
            }
            throw new GameFrameworkException("thread Run"+ m_thread.Name +"Run out");
        }
        public virtual bool Init(string name,int ThreadInternal, Action<float> game_update, byte tupdateInternal)
        {
            bool pret = false;
            do
            {
                if (m_thread != null)
                {
                    throw new GameFrameworkException("m_thread aready userd");
                }
                m_ThreadInternal = ThreadInternal;


                m_thread = new Thread(Run);
                m_thread.Name = name;
                m_thread.IsBackground = true;

                //*************************
                GameUpdate = game_update;
                m_running = true;

                //***********************
                m_utw = new UpdateTimeWatch();
                m_utw.init(tupdateInternal);
                //***************************

                m_thread.Start();
                //m_thread.IsThreadPoolThread = false;

            } while(false);
           
            return pret;
        }



        public void Dispose()
        {
            m_running = false;
            m_utw.Dispose();
            m_thread.Abort();
            m_thread = null;
        }
    }
}
