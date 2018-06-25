using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameFramework;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace GameMain.Net
{
    public class NetWorkManager : GameFrameworkModule
    {

        KcpServer m_KcpServer;
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_KcpServer.State == ServerState.Running)
            {
                m_KcpServer.Update(elapseSeconds, realElapseSeconds);
            }
        }

        public override bool BeforeInit()
        {
            return true;
        }

        public override bool Init()
        {
            /*
            m_ENet_PackServer = new ENetPackServer();
            m_ENet_PackServer.Init();
            m_ENet_PackServer.Start(new IPEndPoint(IPAddress.Parse("192.168.0.90"), 5000));
            */
            var DefaultServerConfig = new ServerConfig
            {
                Ip = "192.168.0.90",
                MaxConnectionNumber = 1000,
                Mode = SocketMode.Udp,
                Name = "Udp Test Socket Server",
                Port = 8000,
                ReceiveBufferSize = NetConstants.SocketBufferSize,
                ClearIdleSession = true,
                ClearIdleSessionInterval = 1,
                IdleSessionTimeOut = 5,
                SendingQueueSize = NetConstants.SendingQueueSize,
                MaxRequestLength = NetConstants.SocketBufferSize,///////////最大请求长度
            };
            m_KcpServer = new KcpServer();
            if (!m_KcpServer.Setup(DefaultServerConfig))
            {
                
                Console.WriteLine("Failed to setup!");
                return false;
            }

            if (!m_KcpServer.Start())
            {
                Console.WriteLine("Failed to start!");
                return false;
            }

            return true;
        }

        public override bool AfterInit()
        {
            return true;
        }

        public override bool BeforeShutdown()
        {
            return true; 
        }

        public override bool Shutdown()
        {
            m_KcpServer.Stop();
            //m_ENet_PackServer.Shutdown();
            return true;
            //throw new NotImplementedException();
        }

        public override bool AfterShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
