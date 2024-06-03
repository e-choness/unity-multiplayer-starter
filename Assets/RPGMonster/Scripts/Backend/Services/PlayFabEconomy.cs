using System.Collections.Generic;
using kart.RPGMonster.Scripts.Backend.Models;
using kart.RPGMonster.Scripts.UI.Models;
using PlayFab;
using PlayFab.EconomyModels;
using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public class PlayFabEconomy : MonoBehaviour
    {
        private static List<CatalogItem> _catalogItems = new();

        private void OnEnable()
        {
            PlayFabResultHandler<SearchItemsResponse>.OnGetResult += PopulateCatalogItems;
        }

        private void OnDisable()
        {
            PlayFabResultHandler<SearchItemsResponse>.OnGetResult -= PopulateCatalogItems;
        }

        public void GetCatalogItems()
        {
            Debug.Log("PlayFab Economy - GetCatalogItems()");
            var request = new SearchItemsRequest();
            PlayFabEconomyAPI.SearchItems(request, 
                PlayFabResultHandler<SearchItemsResponse>.Handle, 
                PlayFabErrorHandler.Handle);
        }

        private void PopulateCatalogItems(SearchItemsResponse response)
        {
            _catalogItems.Clear();
            
            if (response.Items.Count == 0)
            {
                Debug.LogWarning("PlayFabEconomy - PopulateCatalogItems - No catalog items found.");
                return;
            }
            
            foreach (var item in response.Items)
            {
                _catalogItems.Add(item);
            }
            
            ShopUI.UpdateTextArea(_catalogItems);
        }

        public void GetInventory()
        {
            Debug.Log("PlayFab Economy - GetInventory()");
        }

        public void PurchaseItem(string economyViewItem)
        {
            Debug.Log("PlayFab Economy - PurchaseItem()");
        }

        public void BuyFromStore(string economyViewItem)
        {
            Debug.Log("PlayFab Economy - BuyFromStore()");
        }
    }
}
