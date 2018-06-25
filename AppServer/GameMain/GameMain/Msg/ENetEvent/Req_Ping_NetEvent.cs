using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMain
{
    public class Req_Ping_NetEvent : ENetEvent
    {
        public override int Id
        {
            get
            {
                return (int)NetMsgEnum.Msg_Ping;
            }
        }
    }
}
