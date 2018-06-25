using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameFramework;
using GameFramework.Event;

namespace GameMain.Net
{
    public  abstract class PackServer
    {

        /// <summary>
        /// 消息库
        /// psender PackPeer
        /// object  param
        /// </summary>
        public EventMsgPool<PackPeer, object> EventMsgPool
        {
            get
            {
                if (m_Evt_MsgPool == null)
                {
                    m_Evt_MsgPool = new EventMsgPool<PackPeer, object>();
                }
                return m_Evt_MsgPool;
            }
        }
        EventMsgPool<PackPeer, object> m_Evt_MsgPool;

        ConcurrentDictionary<long, PackPeer> m_dict_packpeer;
        public ConcurrentDictionary<long, PackPeer> Dict_PackPeer
        {
            get
            {
                if (m_dict_packpeer == null)
                {
                    m_dict_packpeer = new ConcurrentDictionary<long, PackPeer>();
                }
                return m_dict_packpeer;
            }
        }


        public virtual bool Init()
        {

            return true;
        }
        public virtual void Shutdown()
        {
           

        }
        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {

        }
        public virtual void Start(IPEndPoint address)
        {

        }
        public virtual void Stop()
        {

        }
        public virtual void ParseMsg(PackPeer packpeer,byte[] bytes)
        {

            
            MsgBase ma = new MsgBase();
            if (!ma.Deserialize(bytes))
            {
                AppModules.Instance.LogManager.Log.Error(string.Format("ParseMsg error {0}", packpeer.Get_RemoteEndPoint().ToString() ));
                return ;
            }
            try
            {
                EventMsgPool.FireEvent(ma.MsgHead.m_msgid,packpeer,ma);
            }
            catch (Exception e)
            {
                AppModules.Instance.LogManager.Log.Error(string.Format("MsgId:{0}--exception", ma.MsgHead.m_msgid, e.ToString()));
            }
        }
    }
}
