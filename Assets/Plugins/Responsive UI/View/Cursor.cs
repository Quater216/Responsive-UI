using Plugins.Responsive_UI.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Plugins.Responsive_UI.View
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Image _image;

        private bool _isShowed;

        private void Update()
        {
            if (_isShowed)
            {
                var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, mousePosition.y);
            }
        }

        public async void Show(ItemDefinition definition)
        {
            _image.gameObject.SetActive(true);
            var sprite = Addressables.LoadAssetAsync<Sprite>(definition.SpriteAddress);
            _image.sprite = await sprite.Task;
            _isShowed = true;
        }

        public void Hide()
        {
            _image.gameObject.SetActive(false);
            _image.sprite = null;
            _isShowed = false;
        }
    }
}