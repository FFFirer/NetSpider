using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace NetSpider
{
    public class TestNLog
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public static void WriteLog()
        {
            logger.Info("this is a info log");
            logger.Warn(new Exception("this is a warn log"));
        }
    }
}
