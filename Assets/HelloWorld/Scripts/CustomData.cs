using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public struct CustomData : INetworkSerializable
    {
        public Vector3 Coordinate;
        public FixedString128Bytes Message;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Coordinate);
            serializer.SerializeValue(ref Message);
        }
    }
}