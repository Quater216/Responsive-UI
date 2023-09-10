using System;
using System.Threading.Tasks;
using Plugins.Responsive_UI.View;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Plugins.Responsive_UI.Source
{
    public class EconomyManager : MonoBehaviour
    {
        public static EconomyManager Instance { get; private set; }
        
        [SerializeField] private InventoryWindow _inventoryWindow;

        private void Awake()
        {
            if (Instance == null == false && Instance == this == false)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public async void AddItemToInventory(string id)
        {
            if (_inventoryWindow.IsFull())
                return;
            
            await EconomyService.Instance.PlayerInventory.AddInventoryItemAsync(id);
        }
        
        public async void RemoveItemToInventory(string id)
        {
            await EconomyService.Instance.PlayerInventory.DeletePlayersInventoryItemAsync(id);
        }
    
        public async Task RefreshInventory()
        {
            GetInventoryResult inventoryResult = null;

            try
            {
                inventoryResult = await GetEconomyPlayerInventory();
            }
            catch (EconomyRateLimitedException e)
            {
                inventoryResult = await Utils.RetryEconomyFunction(GetEconomyPlayerInventory, e.RetryAfter);
            }
            catch (Exception e)
            {
                Debug.Log("Problem getting Economy inventory items:");
                Debug.LogException(e);
            }

            if (this == null)
                return;

            EconomyService.Instance.PlayerInventory.PlayersInventoryItemUpdated += _ => 
            {
                _inventoryWindow.Refresh(inventoryResult.PlayersInventoryItems);
            };
            
            _inventoryWindow.Refresh(inventoryResult.PlayersInventoryItems);
        }
    
        private static Task<GetInventoryResult> GetEconomyPlayerInventory()
        {
            var options = new GetInventoryOptions
            {
                ItemsPerFetch = 100
            };
            return EconomyService.Instance.PlayerInventory.GetInventoryAsync(options);
        }
    }
}
