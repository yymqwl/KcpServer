using System.Collections;
using GameFramework.Logging;

namespace GameFramework
{
	public class DebugHandler  
	{
        static ILog m_log;
        public static ILog ILog
        {
            get
            {
                /*
                if(m_log==null)
                {
                    m_log = new UnityLog();
                }*/
                return m_log;
            }
            set
            {
                m_log = value;
            }
        }
        DebugHandler()
        {

        }

        public static void Log(object _object)
        {
            ILog.Debug(_object);
        }
		public static void LogError(object _object)
		{

            ILog.Error(_object);
		
		}
		public static void LogWarning(object _object)
		{
            ILog.Warn(_object);

        }
		public static void LogDebug(object _object)
		{
            ILog.Debug(_object);

        }

	}
}