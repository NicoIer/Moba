using System;
using System.Collections.Generic;
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
            // ServerManager.singleton.server.onConnected += (connectId) =>
            // {
            //     Debug.Log($"connectId:{connectId} connected");
            // };
            //
            // ServerManager.singleton.server.onDisconnected += (connectId) =>
            // {
            //     Debug.Log($"connectId:{connectId} disconnected");
            // };

            ServerManager.singleton.NetStart();
            ServerManager.singleton.server.Listen<CreateObjRequest>(OnCreateNetObj);
            ServerManager.singleton.server.Listen<MoveRequest>(OnMoveRequest);
        }

        private void OnDestroy()
        {
            ServerManager.singleton.server.UnListen<CreateObjRequest>(OnCreateNetObj);
            ServerManager.singleton.server.UnListen<MoveRequest>(OnMoveRequest);
        }

        Dictionary<uint, GameObject> _netObjs = new Dictionary<uint, GameObject>();
        public uint nextId = 0;

        public void OnCreateNetObj(ClientMsg<CreateObjRequest> request)
        {
            CreateObjRequest msg = request.msg;
            GameObject netObj = new GameObject($"NetObj:{nextId}")
            {
                transform =
                {
                    position = new UnityEngine.Vector3(msg.Pos.X, msg.Pos.Y, msg.Pos.Z)
                }
            };
            _netObjs[nextId] = netObj;
            ++nextId;
            CreateObjResponse response = ProtoHandler.Get<CreateObjResponse>();
            response.ObjId = nextId;
            response.Pos = msg.Pos;
            ServerManager.singleton.Send(request.connectId, response);
        }

        public void OnMoveRequest(ClientMsg<MoveRequest> request)
        {
        }
    }
}