using Unity.Netcode.Components;

namespace kart.Kart.Scripts.Multiplayer
{
    public enum AuthorityMode
    {
        Server,
        Client
    }
    public class ClientNetworkTransform : NetworkTransform
    {
        public AuthorityMode authorityMode = AuthorityMode.Client;
        protected override bool OnIsServerAuthoritative() 
            => authorityMode == AuthorityMode.Server;
    }
}
