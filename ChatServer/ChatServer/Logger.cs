using log4net;
using log4net.Config;

namespace ChatServer
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}
