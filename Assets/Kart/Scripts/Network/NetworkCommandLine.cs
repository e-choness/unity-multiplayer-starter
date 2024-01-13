using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace kart.Kart.Scripts.Network
{
    public class NetworkCommandLine : MonoBehaviour
    {
        private NetworkManager _networkManager;

        private void Start()
        {
            _networkManager = GetComponentInParent<NetworkManager>();

            if (Application.isEditor) return;

            var args = GetCommandlineArgs();

            if (args.TryGetValue("-mode", out string mode))
            {
                switch (mode)
                {
                    case "server":
                        _networkManager.StartServer();
                        break;
                    case "host":
                        _networkManager.StartHost();
                        break;
                    case "client":
                        _networkManager.StartClient();
                        break;
                    default:
                        Debug.LogError($"Invalid mode for starting Network Manager. Mode: {mode}");
                        break;
                }
            }
        }

        private Dictionary<string, string> GetCommandlineArgs()
        {
            Dictionary<string, string> argDictionary = new();

            var args = Environment.GetCommandLineArgs();
            
            for (int i = 0; i < args.Length; ++i)
            {
                var arg = args[i].ToLower();
                if (arg.StartsWith("-"))
                {
                    var value = i < args.Length - 1 ? args[i + 1].ToLower(): null;
                    value = value?.StartsWith("-") ?? false ? null : value;
                    
                    argDictionary.TryAdd(arg, value);
                }
            }

            return argDictionary;
        }
    }
}
