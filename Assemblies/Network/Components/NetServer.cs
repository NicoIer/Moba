using UnityEngine;

namespace Moba
{
    [DisallowMultipleComponent]
    public class NetServer : NetManager
    {
        public ServerTransport transport;

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}