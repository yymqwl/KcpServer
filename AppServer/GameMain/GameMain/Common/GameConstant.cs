using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;


namespace GameMain
{
    public class GameConstant : TInstance<GameConstant>
    {

        public const string Log4Name = "AppLog";


        public const float TGameUserLoseTime = 60;
        public const byte TUpdateInternal = 15;// 刷新间隔
        public const byte TThreadInternal = 1;//10 ms刷新间隔


        public const int DefaultBufLen = 1024 * 10;


        public const UInt32 NRoomId_Start = 1000;

        public const int NPLAYERID = 1000;

        /// <summary>
        /// 
        /// ENET
        /// </summary>
        public const int PeerLimit = 1000;
        public const int PeerRetyLimit = 3;
        public const int PeerRetyMinTime = 3000;
        public const int PeerRetyMaxTime = 10000;
    }
}
