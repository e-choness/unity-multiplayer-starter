using System;

namespace kart.RPGMonster.Scripts.Backend.Models
{
    [Serializable]
    public class AuthSettings
    {
        public string BuildId { get; set; }
        public string SessionId { get; set; }
        public string EntityId { get; set; }
        public string DisplayName { get; set; }
        public string NetworkId { get; set; }
    }
}
