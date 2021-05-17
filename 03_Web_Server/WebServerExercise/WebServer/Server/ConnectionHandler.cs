namespace WebServer.Server
{
    using Contracts;
    using Core;
    using Handlers;
    using HTTP;
    using HTTP.Contracts;
    using Routing.Contracts;
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler : IConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));
            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProccessRequestAsync()
        {
            string request = await this.ReadRequest();
            IHttpContext httpContext = new HttpContext(request);
            IHttpResponse response = new HttpHandler(this.serverRouteConfig).Handle(httpContext);
            var toBytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(response.ToString()));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            Console.WriteLine(request);
            Console.WriteLine(response.ToString());

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            string request = string.Empty;
            var data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request += Encoding.ASCII.GetString(data.Array, 0, numBytesRead);

                if (numBytesRead < 1023)
                {
                    break;
                }
            }

            return request;
        }
    }
}
