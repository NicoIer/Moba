using System;
using Nico;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MOBA
{
    public class ClientTest: MonoBehaviour
    {
        private void Start()
        {
            kcp2k.Log.Info = Debug.Log;
            ClientManager.singleton.client.OnConnected += () =>
            {
                Debug.Log($" connected");
            };
            
            ClientManager.singleton.client.OnDisconnected += () =>
            {
                Debug.Log($"disconnected");
            };
        }

        [Button]
        public void StartClient()
        {
            ClientManager.singleton.NetStart();
        }

        [Button]
        public void StopClient()
        {
            ClientManager.singleton.NetStop();
        }

        [Button]
        public void Ping()
        {
            PingMessage pingMessage = ProtoHandler.Get<PingMessage>();
            pingMessage.ClientTime = DateTime.Now.ToUniversalTime().Ticks;
            ClientManager.singleton.Send(pingMessage);
            pingMessage.Return();
        }
        public void Update()
        {
            
        }
    }
}