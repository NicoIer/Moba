// using System;
// using UnityEngine;
//
// namespace Nico
// {
//     public abstract partial class ClientBehavior : MonoBehaviour, IClientBehavior
//     {
//         public uint netObjId { get; set; }
//         public uint behaviorId { get; set; }
//         public ClientObj clientObj { get; set; }
//
//         public void OnConnect(int channelId)
//         {
//             throw new NotImplementedException();
//         }
//
//         public void OnConnect()
//         {
//             throw new NotImplementedException();
//         }
//
//         public void OnDisconnect()
//         {
//             throw new NotImplementedException();
//         }
//
//
// #if UNITY_EDITOR
//
//         private void OnValidate()
//         {
//             // parents中必须有一个NetGameObject
//             Transform cur = transform;
//             while (cur != null)
//             {
//                 if (cur.GetComponent<NetGameObject>() != null)
//                 {
//                     return;
//                 }
//
//                 cur = cur.parent;
//             }
//
//             Debug.LogError($"{nameof(ClientBehavior)} must have a {nameof(NetGameObject)} in parents");
//         }
//
// #endif
//     }
// }