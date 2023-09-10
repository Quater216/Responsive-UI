using System.Collections.Generic;
using System.Linq;
using Plugins.Responsive_UI.Models;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace Plugins.Responsive_UI.View
{
    public class InventoryWindow : AnimatedWindowContainingItems
    {
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
                var definition = EconomyService.Instance.Configuration.GetInventoryItem(item.Key).CustomDataDeserializable.GetAs<ItemDefinition>();
                Cells[i].SetDefinition(definition);
                Cells[i].Quantity = item.Value;
            }
        }

        private void RemoveAll()
        {
            foreach (var inventoryCell in Cells)
            {
                inventoryCell.Clear();
            }
        }

        public bool IsFull()
        {
            return Cells.All(cell => cell.Definition == null == false);
        }
    }
}