namespace UnityToolkit
{
    public interface IServerTransportGetter
    {
        public ServerTransport GetServer();
    }
    
    public interface IClientTransportGetter
    {
        public ClientTransport GetClient();
    }
}