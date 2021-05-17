namespace WebServer.Server.Routing
{
    using Contracts;
    using Core;
    using Handlers.Contracts;
    using System.Collections.Generic;

    public class RoutingContext : IRoutingContext
    {
        private IEnumerable<string> parameters;
        private IRequestHandler requestHandler;

        public RoutingContext(IRequestHandler handler, IEnumerable<string> parameters)
        {
            this.RequestHandler = handler;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters
        {
            get
            {
                return this.parameters;
            }
            private set
            {
                CoreValidator.ThrowIfNull(value, nameof(value));    
                this.parameters = value;
            }
        }

        public IRequestHandler RequestHandler
        {
            get
            {
                return this.requestHandler;
            }
            private set
            {
                CoreValidator.ThrowIfNull(value, nameof(value));
                this.requestHandler = value;
            }
        }
    }
}
