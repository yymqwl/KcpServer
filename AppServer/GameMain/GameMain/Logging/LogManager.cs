using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameFramework;
using GameFramework.Logging;
using log4net;
using ILog = GameFramework.Logging.ILog;


namespace GameMain
{
    public class LogManager : GameFrameworkModule
    {


        public override int Priority
        {
            get
            {
                return 5;
            }
        }
        ILog m_log;
        public ILog Log
        {
            get
            {
                return m_log;
            }
        }


        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new NotImplementedException();
        }

        public override bool BeforeInit()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool Init()
        {

            string ConfigFile = "log4net.config";
            var filePath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"), ConfigFile);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(filePath));
            m_log = new Log4NetLog(log4net.LogManager.GetLogger(GameConstant.Log4Name));

            DebugHandler.ILog = Log;

            return true;
        }

        public override bool AfterInit()
        {
            return true;
            // throw new NotImplementedException();
        }

        public override bool BeforeShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool Shutdown()
        {
            return true;
            // throw new NotImplementedException();
        }

        public override bool AfterShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
