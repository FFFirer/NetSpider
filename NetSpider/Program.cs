using System;
using System.Threading;

namespace NetSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNLog.WriteLog();
            string ConnString = GlobalConfig.Instance.appSettings.MsSqlConnection;
            Console.WriteLine("Hello World!");
            Thread.Sleep(30000);
            string ConnString2 = GlobalConfig.Instance.appSettings.MsSqlConnection;
            
            Console.WriteLine(string.Format("Conn1:{0}", ConnString));
            Console.WriteLine(string.Format("Conn2:{0}", ConnString2));
        }
    }
}
