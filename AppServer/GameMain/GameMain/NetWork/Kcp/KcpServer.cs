using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using GameFramework;


namespace GameMain.Net
{
    public class KcpServer : AppServer<KcpSession, BinaryRequestInfo>
    {


        private ConcurrentDictionary<string, KcpPeer> m_Dict_KcpPeer = new ConcurrentDictionary<string, KcpPeer>(StringComparer.OrdinalIgnoreCase);




        public KcpServer():base(new KcpReceiveFilterFactory())
        {


        }

        protected override void ExecuteCommand(KcpSession session, BinaryRequestInfo requestInfo)
        {
            base.ExecuteCommand(session, requestInfo);
            //////////////////////////////////////////////////////////////////////////
            if (session.KcpPeer!=null)
            {
                session.KcpPeer.Push(requestInfo.Body);//KcpInput(requestInfo.Body);
            }
           

        }
        protected override void OnNewSessionConnected(KcpSession session)
        {
            Console.WriteLine("OnNewSessionConnected"+ session.RemoteEndPoint);

            KcpPeer peer;
            if (!m_Dict_KcpPeer.TryGetValue(session.SessionID, out peer)) //没找到,创建
            {
                //////////////////////////////////////////////////////////////////////////
                peer = new KcpPeer();
                peer.Init(session);
                session.KcpPeer = peer;
                if (!m_Dict_KcpPeer.TryAdd(session.SessionID, peer))
                {
                    DebugHandler.LogError("Error m_Dict_KcpPeer");
                }
                //////////////////////////////////////////////////////////////////////////
            }

            base.OnNewSessionConnected(session);
        }
        protected override void OnSessionClosed(KcpSession session, CloseReason reason)
        {
            Console.WriteLine("OnSessionClosed" + session.RemoteEndPoint);

            KcpPeer peer;
            if (m_Dict_KcpPeer.TryGetValue(session.SessionID, out peer)) //找到
            {
                peer.Close();
                if (!m_Dict_KcpPeer.TryRemove(session.SessionID,out peer))
                {
                    DebugHandler.LogError("Error TryRemove");
                }
            }
            base.OnSessionClosed(session, reason);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            uint iclock =  NetConstants.IClock();
            foreach (var peer in m_Dict_KcpPeer)
            {
                peer.Value.KcpUpdate(iclock);
            }
        }


        protected override void OnStarted()
        {
            base.OnStarted();

        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }
}
