using System;
using Google.Protobuf.Reflection;
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
            ClientManager.singleton.client.Listen<CreateObjResponse>(OnCreateNetObj);
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
        public void CreateObj(Vector3 pos)
        {
            CreateObjRequest request = ProtoHandler.Get<CreateObjRequest>();
            ProtoVector3 protoVector3 = ProtoHandler.Get<ProtoVector3>();
            protoVector3.X = pos.x;
            protoVector3.Y = pos.y;
            protoVector3.Z = pos.z;
            request.Pos = protoVector3;
            ClientManager.singleton.Send(request);
        }
        public void OnCreateNetObj(ServerMsg<CreateObjResponse> response)
        {
            CreateObjResponse msg = response.msg;
            Debug.Log($"OnCreateNetObj,pos:{response.msg.Pos}");
            GameObject netObj = new GameObject($"NetObj:{msg.ObjId}")
            {
                transform =
                {
                    position = new UnityEngine.Vector3(msg.Pos.X, msg.Pos.Y, msg.Pos.Z)
                }
            };
        }
    }
}