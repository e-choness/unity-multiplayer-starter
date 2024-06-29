using System.Collections.Generic;
using System.Linq;
using PlayFab.ClientModels;
using UnityEngine;
using CatalogItem = PlayFab.EconomyModels.CatalogItem;

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

            text += string.Join("\n", items.Select((item, index) => $"{index} | {item.Title.FirstOrDefault().Value} " +
                                                                    $" | Price: {item.PriceOptions.Prices.FirstOrDefault()?.Amounts.FirstOrDefault()?.Amount}"));
            text += "\n\n\n\n\n";
            Debug.Log($"ShopUI - UpdateTextArea() - items: {text}");
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
