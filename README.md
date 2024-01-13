# Unity Multiplayer StarterGame

A code along project to get familiar with Unity multiplayer game setups.

## Prerequisites

- It would be nice to look at official tutorial resources
  1. [Hello World](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/helloworld/)
  2. [Gold Path Module One](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/goldenpath_series/goldenpath_one/)
  3. [Golden Path Module Two](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/goldenpath_series/goldenpath_two/)

- Kart models: download from [Low Poly Models](https://opengameart.org/content/modular-karts), use .blend files under `BLEND` folder. Make sure install Blender for Unity to convert them into assets.
- Input settings can be configured as scriptable objects, a good trick to use and define `InputAction` behaviour before creating them as reusable configurations.
- Useful command line helpers and use cases link: [Here](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/command-line-helper/).
- For Unity 2023.1.x or later, [Multiplayer Play Mode](https://docs-multiplayer.unity3d.com/tools/1.1.0/mppm/) is a better option for testing out server, host and client.
- For Unity 2022.x.x or earilier, using command lines with log output for debug purposes.

  1. Log Server

  ```bash
  <Path to Project>\<Game>.exe -logfile log-server.txt -mode server
  ```

  2. Log Client

  ```bash
  <Path to Project>\<Game>.exe -logfile log-client.txt -mode client
  ```

Unfortunately for now, the logs do not record anything from `Debug.Log()`. A `DebugHelper` outputs anything in a log file on the screen.

## How To Use

1. Add `HelloWorldScene` to `File->Build Settings`.
2. Go to the built executable location. Run console commands for both server and client from `cmd` or Powershell.
3. If server is started first, it has nothing in the scene. Once a client starts, a player object will spawn in both games.

## Network Behaviour Notes

- `NetworkObject` carrier should not be in the scene. Network prefabs handles all `NetworkObject` instances.
- Any `MonoBehaviour` implementing a `NetworkBehaviour` component can override the Netcode method `OnNetworkSpawn()`. The `OnNetworkSpawn()` method fires in response to the `NetworkObject` spawning.

- Having authority capability mapped with `IsServer` or `IsClient` flag will help sync with the rest of the code about which side has authority.
  
```csharp
bool HasAuthority => isServer; // can be set for your whole class or even project
// ...
if (HasAuthority)
    { 
        // take an authoritative decision
        // ...
    }
if (!HasAuthority)
    {
        // ...
    }
```

## Credits

[Low Poly Models](https://opengameart.org/content/modular-karts) made by [@Zsky_01](https://www.patreon.com/Zsky)
