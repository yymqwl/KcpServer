using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework.Event;
using GameMain.Net;

namespace GameMain
{
    public  abstract class NetEvent : GameEventArgs
    {
        public PackPeer m_PackPeer;
        public MsgBase m_msgba;

        public override void Clear()
        {
            m_PackPeer = null;
            m_msgba = null;
        }
    }
}
