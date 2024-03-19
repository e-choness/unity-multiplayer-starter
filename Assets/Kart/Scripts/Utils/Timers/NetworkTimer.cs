namespace kart.Kart.Scripts.Utils.Timers
{
    public class NetworkTimer
    {
        private float _timer;
        public float MinIntervalTime { get; }
        public int CurrentTick { get; private set; }

        public NetworkTimer(float serverTickRate)
        {
            MinIntervalTime = 1.0f / serverTickRate;
        }

        public void Update(float deltaTime)
        {
            _timer += deltaTime;
        }

        public bool ShouldTick()
        {
            if (_timer >= MinIntervalTime)
            {
                _timer -= MinIntervalTime;
                CurrentTick++;
                return true;
            }

            return false;
        }
    }
}