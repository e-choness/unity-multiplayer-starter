using System;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class NetworkTransformTest : NetworkBehaviour
    {
        private void Update()
        {
            if (IsServer)
            {
                var theta = Time.frameCount / 10.0f;
                transform.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
            }
        }
    }
}
