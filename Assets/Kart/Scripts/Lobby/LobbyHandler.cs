using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kart.Kart.Scripts.Utils.Singletons;
using kart.Kart.Scripts.Utils.Timers;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace kart.Kart.Scripts.Lobby {
    [Serializable]
    public enum EncryptionType {
        DTLS, // Datagram Transport Layer Security
        WSS  // Web Socket Secure
    }
    // Note: Also Udp and Ws are possible choices
    
    public class LobbyHandler : PersistentSingleton<LobbyHandler> {
        [SerializeField] string lobbyName = "Lobby";
        [SerializeField] int maxPlayers = 4;
        [SerializeField] EncryptionType encryption = EncryptionType.DTLS;
        
        public string PlayerId { get; private set;  }
        public string PlayerName { get; private set; }
        
        Unity.Services.Lobbies.Models.Lobby _currentLobby;
        string ConnectionType => encryption == EncryptionType.DTLS ? DtlsEncryption : WssEncryption;

        private const float LobbyHeartbeatInterval = 20f;
        private const float LobbyPollInterval = 65f;
        private const string KeyJoinCode = "RelayJoinCode";
        private const string DtlsEncryption = "dtls"; // Datagram Transport Layer Security
        private const string WssEncryption = "wss"; // Web Socket Secure, use for WebGL builds

        readonly CountdownTimer _heartbeatTimer = new(LobbyHeartbeatInterval);
        readonly CountdownTimer _pollForUpdatesTimer = new(LobbyPollInterval);

        async void Start() {
            
            await Authenticate();

            _heartbeatTimer.OnTimerStop += async () => {
                await HandleHeartbeatAsync();
                _heartbeatTimer.Start();
            };
            
            _pollForUpdatesTimer.OnTimerStop += async () => {
                await HandlePollForUpdatesAsync();
                _pollForUpdatesTimer.Start();
            };
        }

        async Task Authenticate() {
            await Authenticate("Player" + Random.Range(0, 1000));
        }

        async Task Authenticate(string playerName) {
            if (UnityServices.State == ServicesInitializationState.Uninitialized) {
                InitializationOptions options = new InitializationOptions();
                options.SetProfile(playerName);

                await UnityServices.InitializeAsync(options);
            }
            
            AuthenticationService.Instance.SignedIn += () => {
                Debug.Log("Signed in as " + AuthenticationService.Instance.PlayerId);
            };

            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                PlayerId = AuthenticationService.Instance.PlayerId;
                PlayerName = playerName;
            }
        }

        public async Task CreateLobby() {
            try {
                Allocation allocation = await AllocateRelay();
                string relayJoinCode = await GetRelayJoinCode(allocation);

                CreateLobbyOptions options = new CreateLobbyOptions {
                    IsPrivate = false
                };
                
                _currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
                Debug.Log("Created lobby: " + _currentLobby.Name + " with code " + _currentLobby.LobbyCode);
                
                _heartbeatTimer.Start();
                _pollForUpdatesTimer.Start();

                await LobbyService.Instance.UpdateLobbyAsync(_currentLobby.Id, new UpdateLobbyOptions {
                    Data = new Dictionary<string, DataObject> {
                        {KeyJoinCode, new DataObject(DataObject.VisibilityOptions.Member, relayJoinCode)}
                    }
                });

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(
                    allocation, ConnectionType));

                NetworkManager.Singleton.StartHost();

            } catch (LobbyServiceException e) {
                Debug.LogError("Failed to create lobby: " + e.Message);
            }
        }

        public async Task QuickJoinLobby() {
            try {
                _currentLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
                _pollForUpdatesTimer.Start();
                
                string relayJoinCode = _currentLobby.Data[KeyJoinCode].Value;
                JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);
                
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(
                    joinAllocation, ConnectionType));
                
                NetworkManager.Singleton.StartClient();
                
            } catch (LobbyServiceException e) {
                Debug.LogError("Failed to quick join lobby: " + e.Message);
            }
        }

        async Task<Allocation> AllocateRelay() {
            try {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers - 1);
                return allocation;
            } catch (RelayServiceException e) {
                Debug.LogError("Failed to allocate relay: " + e.Message);
                return default;
            }
        }

        async Task<string> GetRelayJoinCode(Allocation allocation) {
            try {
                string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                return relayJoinCode;
            } catch (RelayServiceException e) {
                Debug.LogError("Failed to get relay join code: " + e.Message);
                return default;
            }
        }
        
        async Task<JoinAllocation> JoinRelay(string relayJoinCode) {
            try {
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);
                return joinAllocation;
            } catch (RelayServiceException e) {
                Debug.LogError("Failed to join relay: " + e.Message);
                return default;
            }
        }
        
        async Task HandleHeartbeatAsync() {
            try {
                await LobbyService.Instance.SendHeartbeatPingAsync(_currentLobby.Id);
                Debug.Log("Sent heartbeat ping to lobby: " + _currentLobby.Name);
            } catch (LobbyServiceException e) {
                Debug.LogError("Failed to heartbeat lobby: " + e.Message);
            }
        }
        
        async Task HandlePollForUpdatesAsync() {
            try {
                Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_currentLobby.Id);
                Debug.Log("Polled for updates on lobby: " + lobby.Name);
            } catch (LobbyServiceException e) {
                Debug.LogError("Failed to poll for updates on lobby: " + e.Message);
            }
        }
    }
}
