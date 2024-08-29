using Unity.Netcode.Components;
using UnityEngine.Serialization;

namespace kart.Kart.Scripts.Multiplayer
{
    public enum AuthMode
    {
        Server,
        Client
    }
    public class ClientNetworkTransform : NetworkTransform
    {
        public AuthMode authMode = AuthMode.Client;
        protected override bool OnIsServerAuthoritative() 
            => authMode == AuthMode.Server;
    }
}
