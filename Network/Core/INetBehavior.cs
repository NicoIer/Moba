namespace Nico
{
    public interface INetBehavior
    {
        public uint netObjId => clientObj.netId;
        public uint behaviorId { get; set; }
        public ClientObj clientObj { get; set; }
    }

    public interface IClientBehavior : INetBehavior
    {
        void OnConnect();
        void OnDisconnect();
    }

    public interface IServerBehavior : INetBehavior
    {
        void OnConnect(int connectId);
        void OnDisconnect(int connectId);
    }
}