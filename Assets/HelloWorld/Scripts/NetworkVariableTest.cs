using System;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class NetworkVariableTest : NetworkBehaviour
    {
        private NetworkVariable<float> _serverUptimeNetworkVariable = new();
        private float _lastTime = 0.0f;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _serverUptimeNetworkVariable.Value = 0.0f;
                Debug.Log("Server's uptime var initialized to: " + _serverUptimeNetworkVariable.Value);
            }
        }

        private void Update()
        {
            var timeNow = Time.time;
            if (IsServer)
            {
                _serverUptimeNetworkVariable.Value += 0.1f;
                if (timeNow - _lastTime > 0.5f)
                {
                    _lastTime = timeNow;
                    Debug.Log("Server uptime variable has been updated to: " + _serverUptimeNetworkVariable.Value);
                }
            }
        }
    }
}
