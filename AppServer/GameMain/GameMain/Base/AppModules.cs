using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace GameMain
{
    public class AppModules : TInstance<AppModules>
    {
        private LogManager m_LogManager=null;
        public LogManager LogManager
        {
            get
            {
                if (m_LogManager == null)
                {
                    m_LogManager = GameMainEntry.GetModule<LogManager>();
                }
                return m_LogManager;
            }
        }

    }
}
