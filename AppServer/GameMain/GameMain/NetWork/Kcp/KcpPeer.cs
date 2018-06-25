using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;

namespace GameMain.Net
{
    public class KcpPeer : PackPeer
    {
        KCP m_kcp;

        KcpSession m_KcpSession;

        SwitchQueue<byte[]> m_switch_queue = new SwitchQueue<byte[]>(NetConstants.SwitchQueueLen);


        byte[] m_kcp_rv_buf = new byte[NetConstants.SocketBufferSize];


        private bool m_NeedUpdateFlag = true;//
        private UInt32 m_NextUpdateTime = 0;//
        public KcpSession KcpSession
        {
            get
            {
                return m_KcpSession;
            }
            set
            {
                m_KcpSession = value;
            }
        }

        public override void Close()
        {
            base.Close();

            if (KcpSession.Connected)
            {
                KcpSession.Close(SuperSocket.SocketBase.CloseReason.ServerClosing);//服务器关闭端口
            }
            
        }
//         public KcpPeer()
//         {
//             Console.WriteLine("KcpPeer");
//         }
//         ~KcpPeer()
//         {
//             Console.WriteLine("~KcpPeer");
//         }
        public virtual void Init(KcpSession session)
        {
            m_KcpSession = session;

            m_kcp = new KCP(NetConstants.Kcp_Con_V, (byte[] buf, int size) =>
            {
                KcpSession.Send(buf, 0, size);
            });
            m_kcp.NoDelay(1, 10, 2, 1);
            m_kcp.WndSize(NetConstants.PacketSizeLimit, NetConstants.PacketSizeLimit);
            m_kcp.SetMtu(NetConstants.Kcp_Mtu);
            

        }
        public override IPEndPoint Get_RemoteEndPoint()
        {
            return KcpSession.RemoteEndPoint;
        }
        public void Push(byte[] bufs)
        {
            m_switch_queue.Push(bufs);
        }

        public int KcpSend(byte[] bufs)
        {
            return m_kcp.Send(bufs);
        }
        public void KcpUpdate(uint iclock)
        {
            Process_Recv_Queue();
            KcpReceive();
            if (m_NeedUpdateFlag || iclock>= m_NextUpdateTime)
            {
                m_kcp.Update(iclock);
                m_NextUpdateTime = KcpCheck(iclock);
                m_NeedUpdateFlag = false;
            }
        }
        private int KcpInput(byte[] data)
        {
            return m_kcp.Input(data);
        }
        public uint KcpCheck(uint iclock)
        {
            return m_kcp.Check(iclock);
        }

        public void Process_Recv_Queue()
        {
            m_switch_queue.Switch();
            while (!m_switch_queue.Empty())
            {
                var tmpbuf = m_switch_queue.Pop();
                KcpInput(tmpbuf);
                m_NeedUpdateFlag = true;
            }
        }
        public void KcpReceive()
        {
            int irv = -1;
            while (true)
            {
                irv =  m_kcp.Recv(m_kcp_rv_buf);
                if (irv<=0)
                {
                    break;
                }
                else
                {
                    NetDataReader dr = new NetDataReader();
                    dr.SetSource(m_kcp_rv_buf);
                    int iid = dr.PeekInt();
                    Console.WriteLine(iid);

                    KcpSend(m_kcp_rv_buf.CloneRange(0, irv));
                   //  m_kcp_rv_buf.CloneRange(0,irv);
                }
            }
        }

    }
}
