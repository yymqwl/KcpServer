using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;


namespace GameMain.Net
{

    public class KcpReceiveFilter : IReceiveFilter<BinaryRequestInfo>
    {

        public int LeftBufferSize
        {
            get { return 0; }
        }

        public IReceiveFilter<BinaryRequestInfo> NextReceiveFilter
        {
            get { return this; }
        }


        public FilterState State
        {
            get;
            private set;
        }

        public BinaryRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;
            return new BinaryRequestInfo(readBuffer.Length.ToString(), readBuffer.CloneRange(offset, length));
        }

        public void Reset()
        {
        }
    }
}
