using System;
using System.Collections.Generic;

namespace Nico
{
    public partial class ClientObj
    {
        public uint netId;
        private HashSet<IClientBehavior> _clientBehaviors;

        private NetClient _client;
        private uint _currentClientBehaviorId;

        public ClientObj(uint netId, NetClient client)
        {
            this.netId = netId;
            this._client = client;
            _clientBehaviors = new HashSet<IClientBehavior>();
            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;
        }
        ~ClientObj()
        {
            if (_client != null)
            {
                _client.OnConnected -= OnConnected;
                _client.OnDisconnected -= OnDisconnected;
            }
        }

        public void OnDisconnected()
        {
            foreach (var behavior in _clientBehaviors)
            {
                behavior.OnDisconnect();
            }
        }

        public void OnConnected()
        {
            foreach (var behavior in _clientBehaviors)
            {
                behavior.OnConnect();
            }
        }

        public void AddBehavior(IClientBehavior behavior)
        {
            behavior.clientObj = this;
            behavior.behaviorId = _currentClientBehaviorId;
            ++_currentClientBehaviorId;
            _clientBehaviors.Add(behavior);
            //中途添加的行为，需要手动调用OnConnect
            if (_client.connected)
            {
                behavior.OnConnect();
            }
        }

        public void AddBehavior<T>() where T : IClientBehavior, new()
        {
            T behavior = new T();
            AddBehavior(behavior);
        }


        public void RemoveBehavior(IClientBehavior behavior)
        {
            behavior.OnDisconnect();
            _clientBehaviors.Remove(behavior);
        }
    }
}