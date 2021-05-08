namespace WebServer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Server.Enums;

    public class Launcher
    {
        public static void Main(string[] args)
        {

            foreach (var item in Enum.GetValues(typeof(HttpRequestMethod)))
            {
                Console.WriteLine(item);
            }
        }
    }
}
