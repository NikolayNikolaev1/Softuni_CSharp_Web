namespace WebServer.Server.Contracts
{
    using System.Threading.Tasks;

    public interface IConnectionHandler
    {
        Task ProccessRequestAsync();
    }
}
