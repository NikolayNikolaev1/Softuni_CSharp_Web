namespace ByTheCake.Application.Core
{
    using WebServer.Server.HTTP.Contracts;

    public static class CoreValidator
    {
        public static bool CheckForMissingKeys(IHttpRequest request, params string[] keys)
        {
            foreach (string key in keys)
            {
                if (!request.FormData.ContainsKey(key))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckIfNullOrEmpty(params string[] values)
        {
            foreach (string value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
