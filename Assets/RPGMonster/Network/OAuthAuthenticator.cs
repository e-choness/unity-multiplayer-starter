using Mirror;

namespace kart.RPGMonster.Network
{
    /*
        Authenticators:
        https://mirror-networking.com/docs/Components/Authenticators/
        Documentation:
        https://mirror-networking.com/docs/Guides/Authentication.html
        API Reference: https://mirror-networking.com/docs/api/Mirror.
        NetworkAuthenticator.html
    */
    public class OAuthAuthenticator : NetworkAuthenticator
    {
        #region Messages

        public struct AuthRequestMessage : NetworkMessage {}
        
        public struct AuthResponseMessage : NetworkMessage {}
        
        #endregion

        /// <summary>
        /// Called on server from StartServer to initialize the Authenticator
        /// <para> Server message handlers should be registered in this method
        /// </para>
        /// </summary>
        public override void OnStartServer()
        {
            // Register a handler for the authentication request we expect from client.
            NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
        }

        /// <summary>
        /// Called on server from OnServerAuthenticateInternal when a client needs to authenticate
        /// </summary>
        /// <param name="conn">Connection to client</param>
        public override void OnServerAuthenticate(NetworkConnectionToClient conn)
        {
            
        }

        /// <summary>
        /// Called on server when the client's AuthRequestMessage arrives
        /// </summary>
        /// <param name="connection">Connection to client</param>
        /// <param name="request">The message payload</param>
        private void OnAuthRequestMessage(NetworkConnectionToClient connection, AuthRequestMessage request)
        {
            var authResponseMessage = new AuthResponseMessage();
            connection.Send(authResponseMessage);
            
            // Accept the successful authentication
            ServerAccept(connection);
        }

        /// <summary>
        /// Called on client from StartClient to initialize the Authenticator
        /// <para> Client message handlers should be registered in this method.
        /// </para>
        /// </summary>
        public override void OnStartClient()
        {
            NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
        }

        /// <summary>
        /// Called on client from OnClientAuthenticateInternal when a client needs to authenticate
        /// </summary>
        public override void OnClientAuthenticate()
        {
            var authRequestMessage = new AuthRequestMessage();
            NetworkClient.Send(authRequestMessage);
        }
        
        /// <summary>
        /// Called on client when the server's AuthResponseMessage arrives
        /// </summary>
        /// <param name="response">Auth response payload</param>
        private void OnAuthResponseMessage(AuthResponseMessage response)
        {
            // Authentication has been accepted
            ClientAccept();
        }
    }
}
