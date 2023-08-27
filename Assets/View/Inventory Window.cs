using System.Collections.Generic;
using Unity.Services.Economy.Model;
using UnityEngine;

namespace View
{
    public class InventoryWindow : MonoBehaviour, IWindow
    {
        public bool IsOpened { get; private set; }
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private InventoryItemView _template;
        [SerializeField] private Transform _container;

        public void Refresh(List<PlayersInventoryItem> playersInventoryItems)
        {
            RemoveAll();

            if (playersInventoryItems is null) 
                return;

            foreach (var item in playersInventoryItems)
            {
                var inventoryItemView = Instantiate(_template, _container);
                inventoryItemView.SetKey(item.InventoryItemId);
            }
        }

        private void RemoveAll()
        {
            while (_container.childCount > 0)
            {
                DestroyImmediate(_container.GetChild(0).gameObject);
            }
        }

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
    }
}