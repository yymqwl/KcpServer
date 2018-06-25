using System;
using System.Collections.Generic;

namespace GameFramework
{

    /// <summary>
    /// 触发消息事件系统
    /// </summary>
    /// <typeparam name="T0">触发者</typeparam>
    /// <typeparam name="T1">参数类型</typeparam>
    public class EventMsgPool<T0, T1>
    {

        Dictionary<uint ,EventBase<T0,T1> > m_dict_evts;

        public Dictionary<uint,EventBase<T0,T1> > DictEvents
        {
            get
            {
                if (m_dict_evts == null)
                {
                    m_dict_evts = new Dictionary<uint,EventBase<T0,T1> >();
                }

                return m_dict_evts;
            }
        }

        public virtual void Clear()
        {
            m_dict_evts.Clear();
        }

        public virtual void AddEvent(uint id, EventAction<T0, T1> eact)
        {
            EventBase<T0, T1>  evtbase;
            if (DictEvents.TryGetValue(id, out evtbase))
            {
                evtbase.AddListener(eact);
            }
            else//没有事件监听
            {
                evtbase = new EventBase<T0, T1>();
                evtbase.EventId = id;
                evtbase.AddListener(eact);

                DictEvents.Add(id,evtbase);
            }
        }
        public void RemoveEvent(uint id,EventAction<T0, T1> eact)
        {
            EventBase<T0, T1> evtbase;
            if (DictEvents.TryGetValue(id, out evtbase))
            {
                evtbase.RemoveListener(eact);
            }
            else//没有事件监听
            {
                DebugHandler.LogError("Has No Evt");
                return ;
            }
            //if(evtbase.EventActions.Count==0)暂时不删除影响不大

        }

        public virtual void FireEvent(uint id,T0  t0,T1  t1)
        {
            EventBase<T0, T1> evtbase;
            if (DictEvents.TryGetValue(id, out evtbase))
            {
                evtbase.Invoke(t0, t1);
            }
            else
            {
                DebugHandler.LogError(string.Format("FireEvent Has No Evt{0}",id));
            }
        }

    }
}
