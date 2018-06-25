using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Game_Msg;


namespace GameMain
{
    public enum NetMsgEnum
    {
        Msg_Ping = 100,
    }
    public class MsgMap : TInstance<MsgMap>
    {
        private bool m_init = false;

        Dictionary<UInt32, Type> m_dict = new Dictionary<UInt32, Type>();
        public virtual bool init()
        {
            m_init = true;
            m_dict.Clear();
            Add<Req_Ping>(NetMsgEnum.Msg_Ping);
            return m_init;
        }
        public virtual void Add<T>(NetMsgEnum netMsg)
        {
            Dict.Add((UInt32)netMsg, typeof(T));

        }
        
        public Dictionary<UInt32, Type> Dict
        {
            get
            {
                return m_dict;
            }
        }
        public Type GetMsgType(UInt32 id)
        {
            Type tp;
            Dict.TryGetValue(id, out tp);
            return tp;
        }
    }
}
