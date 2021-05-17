namespace WebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();
            this.AddRequestMethodsToRoutes();
            this.InitializeServerConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes
            => routes;

        private void AddRequestMethodsToRoutes()
        {
            var methodTypes = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod requestMethod in methodTypes)
            {
                this.Routes.Add(requestMethod, new Dictionary<string, IRoutingContext>());
            }
        }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var kvp in appRouteConfig.Routes)
            {
                foreach (var requestHandler in kvp.Value)
                {
                    IList<string> args = new List<string>();
                    string parsedRegex = this.ParseRoute(requestHandler.Key, args);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, IList<string> args)
        {
            StringBuilder parsedRegex = new StringBuilder();
            parsedRegex.Append("^");

            if (requestHandlerKey == "/")
            {
                parsedRegex.Append($"{requestHandlerKey}$");
                return parsedRegex.ToString();
            }

            string[] tokens = requestHandlerKey.Split('/');
            this.ParseTokens(args, tokens, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(IList<string> args, string[] tokens, StringBuilder parsedRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                string end = i == tokens.Length - 1 ? "$" : "/";

                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                string pattern = "<\\w+>";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(tokens[i]);

                if (!match.Success)
                {
                    continue;
                }

                string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parsedRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}
