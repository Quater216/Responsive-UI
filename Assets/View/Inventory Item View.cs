using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        
        [Header("Sprites")]
        [SerializeField] private Sprite _swordSprite;
        [SerializeField] private Sprite _shieldSprite;

        public void SetKey(string key)
        {
            switch (key)
            {
                case "SWORD":
                    _iconImage.sprite = _swordSprite;
                    break;

                case "SHIELD":
                    _iconImage.sprite = _shieldSprite;
                    break;

                default:
                    _iconImage.sprite = null;
                    break;
            }
        }
    }
}