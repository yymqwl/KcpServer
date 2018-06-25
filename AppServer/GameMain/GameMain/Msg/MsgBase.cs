using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GameFramework;

namespace GameMain
{


    public class MsgBase
    {       
        MemoryStream m_ms = new MemoryStream();
        MsgHead m_head = new MsgHead();
        public MsgHead MsgHead
        {
            get
            {
                if (m_head == null)
                {
                    m_head = new MsgHead();
                }
                return m_head;
            }
            set { m_head = value; }
        }

        object m_body = null;
        public object MsgBody
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;
            }
        }
        
        public byte[] SerializePackMsg<T>(T msgbody)
        {
            if (msgbody == null)
            {
                AppModules.Instance.LogManager.Log.Error("Null: msgbody");
                return null;
            }
            ResetStrem();
            MsgBody = msgbody;

            BinaryWriter br = new BinaryWriter(m_ms);

            if (!MsgHead.SerializeByStream(m_ms))
            {
                AppModules.Instance.LogManager.Log.Error("MsgBase: Serialize");
                return null;
            }

            ProtoBufUtils.ProtobufSerializeByStream(msgbody, m_ms);
            MsgHead.m_lengh = (uint)m_ms.Length - (uint)MsgHead.Length ;//
            MsgHead.SerializeByStreamPos(m_ms, 0);
            //MsgHead.SerializeByStreamPos(m_ms, sizeof(int));//4个字节的长度

            m_ms.Position = 0;


            return m_ms.ToArray();
        }
        public byte[] Serialize<T>(T  msgbody)
        {
            if (msgbody == null)
            {
                AppModules.Instance.LogManager.Log.Error("Null: msgbody");
                return null;
            }
            ResetStrem();
            MsgBody = msgbody;

            if (!MsgHead.SerializeByStream(m_ms))
            {
                AppModules.Instance.LogManager.Log.Error("MsgBase: Serialize"  );
                return null;
            }

            ProtoBufUtils.ProtobufSerializeByStream(msgbody, m_ms);
            MsgHead.m_lengh = (uint)m_ms.Length - (uint)MsgHead.Length;
            MsgHead.SerializeByStreamPos(m_ms,0);
            return m_ms.ToArray();
        }
        public void ResetStrem()
        {
            m_ms.Position = 0;
            m_ms.SetLength(0);
        }


        public bool Deserialize(byte[] bufs)
        {
           


            if (bufs.Length < MsgHead.Length)
            {
                AppModules.Instance.LogManager.Log.Error("MsgHead.length error");

                return false;
            }
            m_ms = new MemoryStream(bufs);
            MsgHead.DeserializeByStream(m_ms);

            int packlen = MsgHead.Length + (int)MsgHead.m_lengh;
            if (packlen != bufs.Length)
            {
                //////////////////长度数据出错
                /////////////////////////
                AppModules.Instance.LogManager.Log.Error("length error");
                return false;

            }
            Type  tp = MsgMap.Instance.GetMsgType(MsgHead.m_msgid);
            if (tp == null)
            {
                AppModules.Instance.LogManager.Log.Error("NULL msgtp"+ MsgHead.m_msgid);
                return false;
            }

            object  resultobj = ProtoBufUtils.ProtobufDeserializeByStream(m_ms, tp);
            if (resultobj == null)
            {
                AppModules.Instance.LogManager.Log.Error("NULL resultobj");
                return false;
            }
            MsgBody = resultobj;

            return true;

        }

    }
}
