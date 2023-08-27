using System.Collections.Generic;
using System.Linq;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace View
{
    public class InventoryWindow : MonoBehaviour, IWindow
    {
        public bool IsOpened { get; private set; }

        [SerializeField] private Canvas _canvas;
        [SerializeField] private InventoryCellView[] _inventoryCells;

        public void Open()
        {
            _canvas.enabled = true;
            IsOpened = true;
        }

        public void Close()
        {
            _canvas.enabled = false;
            IsOpened = false;
        }

        public void Refresh(List<PlayersInventoryItem> playersInventoryItems)
        {
            RemoveAll();

            if (playersInventoryItems is null)
                return;

            Dictionary<string, int> itemsWithQuantities = new();

            for (int i = 0; i < playersInventoryItems.Count - 1; i++)
            {
                if (i == 0)
                    itemsWithQuantities.Add(playersInventoryItems[i].InventoryItemId, 1);
                
                var nextItem = playersInventoryItems[i + 1];

                if (itemsWithQuantities.TryGetValue(nextItem.InventoryItemId, out var quantity))
                {
                    itemsWithQuantities[nextItem.InventoryItemId] = quantity + 1;
                }
                else
                {
                    itemsWithQuantities.Add(nextItem.InventoryItemId, 1);
                }
            }

            for (int i = 0; i < itemsWithQuantities.Count; i++)
            { 
                var item = itemsWithQuantities.ElementAt(i); 
                _inventoryCells[i].SetKey(item.Key);
                _inventoryCells[i].Quantity = item.Value;
            }
        }

        private void RemoveAll()
        {
            foreach (var inventoryCell in _inventoryCells)
            {
                inventoryCell.SetKey(null);
            }
        }
    }
}