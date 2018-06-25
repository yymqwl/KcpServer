using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMain.Net
{
    public static class NetConstants
    {

        public const int SocketBufferSize = 1024 * 8; //1mb


        public const int Kcp_Con_V = 0xff;

        public const int PacketSizeLimit = 1024 * 4;//ushort.MaxValue;

        public const int SwitchQueueLen = 100;

        public const int SendingQueueSize = 100;


        public const int Kcp_Mtu = 1400;

        public const int MaxConnectionNumber = 1000;
        private static readonly DateTime utc_time = new DateTime(1970, 1, 1);
        public static UInt32 IClock()
        {
            return (UInt32)(Convert.ToInt64(DateTime.UtcNow.Subtract(utc_time).TotalMilliseconds) & 0xffffffff);
        }
    }
}
