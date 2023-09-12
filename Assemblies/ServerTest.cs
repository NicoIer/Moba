using System;
using Nico;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MOBA
{
    public class ServerTest : MonoBehaviour
    {
        private void Start()
        {
            kcp2k.Log.Info = Debug.Log;
            ServerManager.singleton.server.onConnected += (connectId) =>
            {
                Debug.Log($"connectId:{connectId} connected");
            };

            ServerManager.singleton.server.onDisconnected += (connectId) =>
            {
                Debug.Log($"connectId:{connectId} disconnected");
            };
        }

        [Button]
        public void StartServer()
        {
            ServerManager.singleton.NetStart();
        }

        [Button]
        public void StopServer()
        {
            ServerManager.singleton.NetStop();
        }

        [Button]
        public void Listen()
        {
            ServerManager.singleton.server.Listen<PingMessage>(OnPing);
        }

        [Button]
        public void UnListen()
        {
            ServerManager.singleton.server.UnListen<PingMessage>(OnPing);
        }

        public void OnPing(ClientMsg<PingMessage> ping)
        {
            Debug.Log($"{nameof(PingMessage)} form connectId:{ping.connectId} clientTime:{ping.msg.ClientTime}");
        }
    }
}