namespace WebServer.Server.Core
{
    using System;

    public static class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string str, string name)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
