using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nico
{
    [DisallowMultipleComponent]
    public sealed partial class NetGameObject : MonoBehaviour
    {
        private ClientObj _obj;

        private ClientManager _manager => ClientManager.singleton;

        // private ServerObj _obj;
        public void AddBehavior(ClientBehavior behavior) => _obj.AddBehavior(behavior);
        public void RemoveBehavior(ClientBehavior behavior) => _obj.RemoveBehavior(behavior);

        private void Start()
        {
            _manager.RegisterHandler<CreateObjResponse>(OnCreateResponse);
            _manager.client.OnConnected += OnConnected;
            _obj = new ClientObj(0, _manager.client);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if(_manager == null) return;
            _manager.UnRegisterHandler<CreateObjResponse>(OnCreateResponse);
            _manager.client.OnConnected -= OnConnected;
        }

        public void OnConnected()
        {
            CreateObjRequest request = ProtoHandler.Get<CreateObjRequest>();
            Vector3 pos = transform.position;
            request.X = pos.x;
            request.Y = pos.y;
            request.Z = pos.z;
            _manager.Send(request);
            request.Return();
            Debug.Log("OnConnected");
        }

        public void OnCreateResponse(ServerPack<CreateObjResponse> pack)
        {
            Debug.Log("OnCreateResponse");
            _obj.id = pack.msg.Id;
            transform.position = new Vector3(pack.msg.X, pack.msg.Y, pack.msg.Z);
            Debug.Log($"OnCreateResponse,netId:{_obj.id}");
            gameObject.SetActive(true);
        }
    }

#if UNITY_EDITOR
    public partial class NetGameObject
    {
        public void OnValidate()
        {
            //子物体和父物体中不能再有NetGameObject
            foreach (Transform child in transform)
            {
                if (child.GetComponent<NetGameObject>() != null)
                {
                    Debug.LogError($"{nameof(NetGameObject)} can not be child of {nameof(NetGameObject)}");
                }
            }
        }
    }
#endif
}