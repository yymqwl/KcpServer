using System;
using System.Collections.Generic;


namespace GameFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T0">psend</typeparam>
    /// <typeparam name="T1">param</typeparam>
    /// <returns></returns>
    public delegate bool DelTrigger<T0, T1>(T0 psend, T1 param);
    public class Trigger
    {
        DelTrigger<object,object> m_del_pred;
        int m_trgtimes;
        public DelTrigger<object, object> TriggerCondition
        {
            get
            {
                if (m_del_pred == null)
                {
                    m_del_pred = new DelTrigger<object, object>(delegate(object psend, object param) { return true; });
                }
                return m_del_pred; 
            }
            set { m_del_pred = value; }
        }
        
        public int TriggerTimes //大于0判断是否触发完毕
        {
            get { return m_trgtimes; }
            set { m_trgtimes = value; }
        }
        public virtual bool TryCheckTrigger(object psend, object param)
        {
            if (TriggerCondition(psend,param))
            {
                m_trgtimes--;
                return true;
            }
                return false;
        }

        public virtual void Reset()
        {
            TriggerTimes = 1;
        }

        /// <summary>
        /// 返回true 已经触发完毕
        /// </summary>
        /// <returns></returns>
        public virtual bool Hastrigger()
        {
            return TriggerTimes < 1;
        }
       
    }
}
