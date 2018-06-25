using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameMain
{
    public interface ISerializable
    {
        bool DeserializeByStream(MemoryStream ms);
        bool SerializeByStream(MemoryStream ms);


    }
}
