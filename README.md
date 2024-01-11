# Unity Multiplayer StarterGame

A code along project to get familiar with Unity multiplayer game setups.

## Prerequisites

- Kart models: download from [Low Poly Models](https://opengameart.org/content/modular-karts), use .blend files under `BLEND` folder. Make sure install Blender for Unity to convert them into assets.
- Input settings can be configured as scriptable objects, a good trick to use and define `InputAction` behaviour before creating them as reusable configurations.
- Get Unity Netcode started guide link: [Here](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/get-started-ngo/).
- Useful command line helpers and use cases link: [Here](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/command-line-helper/).
- For Unity 2023.1.x or later, [Multiplayer Play Mode](https://docs-multiplayer.unity3d.com/tools/1.1.0/mppm/) is a better option for testing out server, host and client.
- `NetworkObject` carrier should not be in the scene. Network prefabs handles all `NetworkObject` instances.

Command Lines with Log output

- Log Server

```bash
<Path to Project>\<Game>.exe -logfile log-server.txt -mode server
```

- Log Client

```bash
<Path to Project>\<Game>.exe -logfile log-client.txt -mode client
```
Unfortunately for now, the logs do not record anything from `Debug.Log()`.

## Credits

[Low Poly Models](https://opengameart.org/content/modular-karts) made by [@Zsky_01](https://www.patreon.com/Zsky)
