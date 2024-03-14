using System;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class NetworkTransformTest : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            gameObject.name += HelloWorldManager.Mode;
            transform.position = Vector3.up;
        }
        private void Update()
        {
            if (IsServer)
            {
                var theta = Time.frameCount / 10.0f;
                transform.position = new Vector3((float)Math.Cos(theta), 2.0f, (float)Math.Sin(theta));
            }
        }
    }
}
