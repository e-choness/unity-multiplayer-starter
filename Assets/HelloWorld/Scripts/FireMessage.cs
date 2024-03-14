using Unity.Netcode;

namespace kart.HelloWorld.Scripts
{
    public struct FireMessage: INetworkSerializable, System.IEquatable<FireMessage>
    {
        public int RandomNum;
        public string Message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsReader)
            {
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out RandomNum);
                reader.ReadValueSafe(out Message);
            }
            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(RandomNum);
                writer.WriteValueSafe(Message);
            }
        }

        public bool Equals(FireMessage other)
        {
            return RandomNum == other.RandomNum && Message == other.Message;
        }
    }
}