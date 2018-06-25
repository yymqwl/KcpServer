using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using ProtoBuf;


namespace GameMain.Lockstep
{
    [ProtoContract]
    public class Command
    {
        [ProtoMember(1)]
        public byte Command_Id { get; set; }

        [ProtoMember(2)]
        Dictionary<byte, ProtoObject> m_dict_param;
        public Dictionary<byte, ProtoObject> Dict_Param
        {
            get
            {
                if(m_dict_param == null)
                {
                    m_dict_param = new Dictionary<byte, ProtoObject>();
                }
                return m_dict_param;
            }
        
        }
    }
}
