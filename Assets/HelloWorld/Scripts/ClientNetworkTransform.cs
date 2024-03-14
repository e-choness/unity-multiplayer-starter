using Unity.Netcode.Components;

namespace kart.HelloWorld.Scripts
{
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}