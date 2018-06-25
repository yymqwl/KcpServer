using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMain;

namespace AppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainEnter me = new MainEnter();
            me.Enter();
            me.Exit();
        }
    }
}
