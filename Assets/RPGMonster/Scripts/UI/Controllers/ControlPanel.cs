using kart.RPGMonster.Scripts.Backend.Services;
using kart.RPGMonster.Scripts.UI.Models;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

namespace kart.RPGMonster.Scripts.UI.Controllers
{
    public class ControlPanel : MonoBehaviour
    {
        // Menu selection
        private MenuSelection _selection = MenuSelection.RootMenu;

        // Data models
        private static readonly PlayerAccount Account = new();
        private static readonly EconomyElements EconomyElements = new();
        
        // Components
        private PlayFabAuth _playFabAuth;
        private GameObject _azure;
        private PlayFabEconomy _playFabEconomy;

        void Start()
        {
            _playFabAuth = GameObject.Find("PlayFabAuth").GetComponent<PlayFabAuth>();
            _azure = GameObject.Find("Azure");
            _playFabEconomy = GameObject.Find("PlayFabEconomy").GetComponent<PlayFabEconomy>();
        }

        private void OnGUI()
        {
            switch (_selection)
            {
                case MenuSelection.RootMenu:
                    AddWindow(_selection, OptionsWindow);
                    break;
                case MenuSelection.PlayFabLogin:
                    AddWindow(_selection, LoginWithPlayFabWindow);
                    break;
                case MenuSelection.PlayFabLoginWithUserPass:
                    AddWindow(_selection, LoginWithUserPassWindow);
                    break;
                case MenuSelection.AzureLogin:
                    AddWindow(_selection, LoginWithAzureWindow);
                    break;
                case MenuSelection.PlayFabEconomy:
                    AddWindow(_selection, PlayFabEconomyWindow);
                    break;
                default:
                    Debug.LogWarning("ControlPanel - Not a valid login method.");
                    break;
            }
        }

        private void OptionsWindow(int windowID)
        {
            AddButton("Login With PlayFab");
            AddButton("Login With PlayFab User Pass");
            AddButton("Login With Azure");
            AddButton("PlayFab Economy");
            // TODO: Add additional buttons
            
            GUILayout.Space(10);
        }

        private void LoginWithPlayFabWindow(int windowID)
        {
            GUILayout.Label("Display name:");
            Account.displayName = GUILayout.TextField(Account.displayName, 20);
            
            if (!Account.displayName.IsNullOrWhiteSpace())
            {
                if (GUILayout.Button("Login as Guest"))
                {
                    _playFabAuth.LoginWithCustomId(Account.displayName);
                    _selection = MenuSelection.RootMenu;
                }
                AddButton("Login with Username and Password");
            }

            AddButton("Cancel");
        }

        private void LoginWithUserPassWindow(int windowID)
        {
            GUILayout.Label("Username:");
            Account.username = GUILayout.TextField(Account.username, 25);
            GUILayout.Label("Password:");
            Account.password = GUILayout.PasswordField(Account.password, '*', 20);
            
            AddButton("Cancel");
        }
        
        private void LoginWithAzureWindow(int windowID)
        {
            AddButton("Cancel");
        }

        private void PlayFabEconomyWindow(int windowID)
        {
            InitEconomyView();
            
            GUILayout.Space(10);

            AddCatalogButton();

            AddInventoryButton();
            
            GUILayout.Space(10);
            
            AddPurchaseItemButton(100);

            GUILayout.Space(10);
            
            AddBuyFromStore();
            
            GUILayout.Space(10);
            
            AddButton("Cancel");
        }

        private void InitEconomyView()
        {
            if(EconomyElements.Title != "") GUILayout.Label(EconomyElements.Title);

            EconomyElements.TextArea = ShopUI.GetTextArea();
            EconomyElements.TextArea = GUILayout.TextArea(EconomyElements.TextArea, 200);

            EconomyElements.VirtualCurrencyLabel = ShopUI.GetVirtualCurrencyLabel();
            if(EconomyElements.VirtualCurrencyLabel != "") GUILayout.Label(EconomyElements.VirtualCurrencyLabel);
        }

        private void AddCatalogButton()
        {
            if (GUILayout.Button("Get Catalog Items"))
            {
                _playFabEconomy.GetCatalogItems();
                EconomyElements.Title = "Catalog Items";
            }
        }

        private void AddInventoryButton()
        {
            if (GUILayout.Button("Get Player Inventory + Virtual Currency"))
            {
                _playFabEconomy.GetInventory();
                EconomyElements.Title = "Player Inventory";
            }
        }

        private void AddPurchaseItemButton(int number)
        {
            if (GUILayout.Button($"Purchase this item {number}:"))
            {
                _playFabEconomy.PurchaseItem(EconomyElements.Item);
            }
            EconomyElements.Item = GUILayout.TextField(EconomyElements.Item, 100);
        }

        private void AddBuyFromStore()
        {
            if (GUILayout.Button("Buy From Store"))
            {
                _playFabEconomy.BuyFromStore(EconomyElements.Item);
            }
        }

        private void AddWindow(MenuSelection selection, GUI.WindowFunction windowFunc)
        {
            GUILayout.Window(0, new Rect(0, 0, 300, 0), windowFunc, GetText(selection));
        }

        private void AddButton(string buttonText)
        {
            if (GUILayout.Button(buttonText))
            {
                _selection = GetSelection(buttonText);
            }
        }

        private static MenuSelection GetSelection(string buttonText)
        {
            return buttonText switch
            {
                "Login With PlayFab" => MenuSelection.PlayFabLogin,
                "Login With PlayFab User Pass" => MenuSelection.PlayFabLoginWithUserPass,
                "Login With Azure" =>MenuSelection.AzureLogin,
                "PlayFab Economy" => MenuSelection.PlayFabEconomy,
                "Cancel" => MenuSelection.RootMenu,
                _ => MenuSelection.RootMenu
            };
        }

        private static string GetText(MenuSelection selection)
        {
            return selection switch
            {
                MenuSelection.PlayFabLogin => "Login With PlayFab",
                MenuSelection.PlayFabLoginWithUserPass => "Login With PlayFab User Pass",
                MenuSelection.AzureLogin => "Login With Azure",
                MenuSelection.PlayFabEconomy => "PlayFab Economy",
                MenuSelection.RootMenu => "Cancel",
                _ => string.Empty
            };
        }
    }
}
