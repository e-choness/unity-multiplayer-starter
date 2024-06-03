using System.Collections.Generic;
using System.Linq;
using PlayFab.ClientModels;

namespace kart.RPGMonster.Scripts.UI.Models
{
    public static class ShopUI
    {
        private static string _textArea = "\n\n\n\n\n";
        private static string _virtualCurrencyLabel = "";
        
        public static void UpdateTextArea(List<ItemInstance> items)
        {
            var text = "";
            
            text += string.Join("\n", items.Select((item, index) => $"{index} | {item.DisplayName}"));

            text += "\n\n\n\n\n";
            _textArea = text;
        }

        public static void UpdateTextArea(List<CatalogItem> items)
        {
            var text = "";

            text += string.Join("\n", items.Select((item, index) => $"{index} | {item.DisplayName} " +
                                                                    $"{(item.VirtualCurrencyPrices.Count!=0 ? item.VirtualCurrencyPrices["MC"]+ "MC" : "N/A")}"));
            text += "\n\n\n\n\n";
            _textArea = text;
        }
        
        public static string GetTextArea()
        {
            return _textArea;
        }
        
        public static void UpdateVirtualCurrency(VirtualCurrencyInfo info)
        {
            _virtualCurrencyLabel = info.ToString();
        }
        
        public static string GetVirtualCurrencyLabel()
        {
            return _virtualCurrencyLabel;
        }
    }
}
