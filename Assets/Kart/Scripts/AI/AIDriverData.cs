using UnityEngine;

namespace kart.Kart.Scripts.AI
{
    [CreateAssetMenu(fileName = "AIDriverData", menuName = "Kart/AIDriverData")]
    public class AIDriverData : ScriptableObject
    {
        public float wayPointRange = 20.0f;
        public float cornerRange = 50.0f;
        public float brakeRange = 80.0f;
        public float spinAngleRange = 100.0f;
        public float driftSpeed = 0.5f;
        public float driftTime = 0.5f;
    }
}