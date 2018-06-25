using System;
using System.Collections.Generic;


namespace GameFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="psender">发送者</param>
    /// <param name="param">参数</param>
    public delegate void EventAction<T0,T1>(T0 psender,T1 param);

    public class EventBase<T0, T1>
    {
        uint m_evtid;
        List<EventAction<T0, T1>> m_ls_evtacts;
        public uint EventId
        {
            get { return m_evtid; }
            set { m_evtid = value; }
        }
        public List<EventAction<T0, T1>> EventActions
        {
            get
            {
                if (m_ls_evtacts == null)
                {
                    m_ls_evtacts = new List<EventAction<T0, T1>>();
                }
                return m_ls_evtacts;
            }
            set
            {
                m_ls_evtacts = value;
            }
        }
        public EventBase()
        { }
        public virtual void AddListener(EventAction<T0, T1> call)
        {
            if (EventActions.Contains(call))
            {

                DebugHandler.LogError("EventActions Contains cal");
                return;
            }
            EventActions.Add(call);
        }
        public virtual void RemoveListener(EventAction<T0, T1> call)
        {
            if (EventActions.Contains(call))
            {
                EventActions.Remove(call);
            }
            else
            {
                DebugHandler.LogError("EventActions not Contains cal");
            }
        }

        public virtual void Invoke(T0 psender, T1 param)
        {
            foreach (EventAction<T0, T1> evtact in EventActions)
            {
                evtact(psender, param);
            }
        }
    }
}
