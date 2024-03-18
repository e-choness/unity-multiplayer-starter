using UnityEngine;

namespace kart.Kart.Scripts.System
{
    [CreateAssetMenu(fileName = "WayPointsData", menuName = "Kart/WayPointsData")]
    public class WayPoints : ScriptableObject
    {
        public Transform[] checkPoints;
        public Transform[] spawnPoints;
    }
}