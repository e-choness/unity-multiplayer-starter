using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public class PlayFabEconomy : MonoBehaviour
    {
        public void GetCatalogItems()
        {
            Debug.Log("PlayFab Economy - GetCatalogItems()");
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
