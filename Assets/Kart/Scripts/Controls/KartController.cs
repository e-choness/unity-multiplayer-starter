using System;
using UnityEngine;

namespace kart
{
    [Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool isSteering;
        public WheelFrictionCurve originalForwardFriction;
        public WheelFrictionCurve originalSidewayFriction;
    }
    public class KartController : MonoBehaviour
    {
        [Header("Axle Information")]
        [SerializeField] private AxleInfo[] axleInfo;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
