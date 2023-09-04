using UnityEngine;

namespace Moba
{
    [DisallowMultipleComponent]
    public class NetClient : NetManager
    {
        public ClientTransport transport = new KcpClientTransport(KcpUtil.defaultConfig, 24419);

        public override void Start()
        {
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}