# Unity Multiplayer StarterGame

A code along project to get familiar with Unity multiplayer game setups.

## Unity Version and Packages

- [Unity 6 Beta (6000.0.0b16)](https://unity.com/releases/editor/beta)
- [VContainer - A lightweight Unity dependency injection solution](https://github.com/hadashiA/VContainer)
- [SceneReference - An inspector scene reference tool](https://github.com/starikcetin/Eflatun.SceneReference)

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

The Debugging logs on `NetworkVariableTest` works and outputs server uptime information. A `DebugHelper` outputs debugging logs on the screen as well.

## How To Use

1. Add `HelloWorldScene` to `File->Build Settings`.
2. Go to the built executable location. Run console commands for both server and client from `cmd` or Powershell.
3. If server is started first, it has nothing in the scene. Once a client starts, a player object will spawn in both games.
4. Install `Multiplayer Tools` from Unity Package Manager Registry. Not mandatory, but very helpful for debugging and monitoring network traffic.
   - Profiler can be accessed from `Window`->`Analysis`->`Profiler`, and scrolldown to `NGO Messages` and `NGO Objects` sections.
   - Create an empty object in the scene and attach `RuntimeNetstatsMonitor` component for runtime data monitoring.

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

- `NetworkVariable` take Generics and serialize them intelligently. If using a constum data structure as `NetworkVariable` type, it should implement `INetworkSerializable` and its `NetworkSerialize` method.

```csharp
public struct CustomData : INetworkSerializable{
    public int Id;
    public bool IsOwner;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T: IReaderWriter{
        serializer.SerializeValue(ref Id);
        serializer.SerializeValue(ref IsOwner);
    }
}
```

- `[ServerRpc]` attribute marks events being sent to the server. The method is forced to have `ServerRpc` suffix, otherwise it won't compile.
- `[ClientRpc]` attribute marks events executed by the server and being sent to the client. he method is forced to have `ClientRpc` suffix, otherwise it won't compile.

## Credits

- [Multiplayer Kart](https://github.com/adammyhre/Unity-Multiplayer-Kart) made by [adammyhre](https://github.com/adammyhre)
- [Low Poly Models](https://opengameart.org/content/modular-karts) made by [@Zsky_01](https://www.patreon.com/Zsky)
