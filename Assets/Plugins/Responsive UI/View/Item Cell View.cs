using Plugins.Responsive_UI.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Plugins.Responsive_UI.View
{
    public class ItemCellView : MonoBehaviour
    {
        public ItemDefinition Definition { get; private set; }
        public int Quantity { get; set; }
        
        [SerializeField] private Image _iconImage;

        public void SetDefinition(ItemDefinition definition)
        {
            Definition = definition;
            UpdateView();
        }

        private async void UpdateView()
        {
            var sprite = Addressables.LoadAssetAsync<Sprite>(Definition.SpriteAddress).Task;
            _iconImage.sprite = await sprite;
        }

        public void Clear()
        {
            Definition = null;
            Quantity = 0;
            _iconImage.sprite = null;
        }
    }
}