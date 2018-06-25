using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;


namespace GameMain
{


    public class MsgHead : ISerializable
    {
       
       
        public UInt32 m_msgid;//消息头
        public UInt32 m_lengh;
        //public byte m_compress;//是否压缩
        //public byte m_serialize_type; //序列化类型/ 

        public enum Servilize_Type
        {
            E_ProtoBuf,
            E_Json
        }

 
        public static int Length
        {
            get
            {
                return (sizeof(UInt32) * 2);
            }
        }

        public bool DeserializeByStream(MemoryStream ms)
        {
            
            if (  ( ms.Length-ms.Position )< Length)
            {
                return false;
            }
            BinaryReader br = new BinaryReader(ms);
            m_msgid = br.ReadUInt32();
            m_lengh = br.ReadUInt32();
            return true;
        }

        public bool SerializeByStream(MemoryStream ms)
        {
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(m_msgid);
            bw.Write(m_lengh);         
            return true;
        }
        public bool SerializeByStreamPos(MemoryStream ms,int pos)
        {
            BinaryWriter bw = new BinaryWriter(ms);
            ms.Position = pos;
            bw.Write(m_msgid);
            bw.Write(m_lengh);
            return true;
        }


    }

}
