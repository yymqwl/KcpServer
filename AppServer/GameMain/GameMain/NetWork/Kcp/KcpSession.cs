
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace GameMain.Net
{
    public class KcpSession: AppSession<KcpSession, BinaryRequestInfo>
    {

        KcpPeer m_peer;

        public KcpPeer KcpPeer
        {
            get
            {
                return m_peer;
            }
            set
            {
                m_peer = value;
            }
        }

    }
}
