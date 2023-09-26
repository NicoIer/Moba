namespace UnityToolkit
{
    public interface INetObj
    {
    }

    public abstract class NetObj : INetObj
    {
        public uint id;
    }

    public class ServerObj : NetObj
    {
    }


    public class ClientObj : NetObj
    {
    }
}