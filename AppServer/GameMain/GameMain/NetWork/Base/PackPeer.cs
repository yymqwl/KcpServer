using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameMain.Net
{
    public abstract class PackPeer
    {
       
        public virtual int Id
        {
            get
            {
                return GetHashCode();
            }
        }
        public abstract IPEndPoint Get_RemoteEndPoint();

        public virtual void Close()
        {

        }

    }
}
