namespace kart.RPGMonster.Scripts.UI.Models
{
    public class VirtualCurrencyInfo
    {
        public string Name { get; set; } = "";
        public int Amount { get; set; } = 0;

        public override string ToString()
        {
            return $"You have {Amount} of {Name}";
        }
    }
}
