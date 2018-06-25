using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework;

namespace GameMain
{
    public abstract class BaseTask
    {
        protected object m_threadLock = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        protected BaseTask(int interval)
        {
            Running = false;
            IntevalTicks = new TimeSpan(0, 0, 0, 0, interval).Ticks;
            Interval = 1000;
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Running
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Timing
        {
            get;
            set;
        }

        /// <summary>
        /// 间隔时间(毫秒)
        /// </summary>
        public int Interval
        {
            get;
            set;
        }

        /// <summary>
        /// 间隔刻度
        /// </summary>
        public long IntevalTicks
        {
            get;
            set;
        }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public long NextTriggerTime
        {
            get;
            set;
        }

 
        protected abstract void SetNextTrigger();
    


        public virtual bool Check(ref long ticks)
        {
           
            if (Running || ticks < NextTriggerTime)
            {
                return false; 
            }
            SetNextTrigger();
            return true;
        }

        /// <summary>
        /// 获取定时时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetTiming()
        {
            DateTime time = DateTime.MinValue;
            if (DateTime.TryParse(Timing, out time))
            {
            }
            return time;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="obj"></param>
        public void Proccess(object obj)
        {
            try
            {
                lock (m_threadLock)
                {
                    Running = true;
                    DoExecute(obj);
                    Running = false;
                }

            }
            catch (Exception ex)
            {
                throw new GameFrameworkException("任务[" + TaskName + "]出错" + ex.ToString());
                //AppModuleManager.Instance.LogModule.Log.Debug("任务[" + TaskName + "]出错" + ex.ToString());
            }
        }

        /// <summary>
        /// 执行任务处理
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void DoExecute(object obj);

    }
}
