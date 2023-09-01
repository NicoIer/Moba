using UnityEngine;

namespace Moba
{
    [DisallowMultipleComponent]
    public class NetClient : NetManager
    {
        public ClientTransport transport;

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