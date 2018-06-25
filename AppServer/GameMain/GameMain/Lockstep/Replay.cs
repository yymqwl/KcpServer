using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace GameMain.Lockstep
{
    [ProtoContract]
    public class Replay
    {
        [ProtoMember(1)]
        public List<Frame> m_ls_frame;
        [ProtoMember(2)]
        public string m_Name;
        [ProtoMember(3)]
        public DateTime m_Date;
    }
}
