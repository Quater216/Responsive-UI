using Plugins.Responsive_UI.Source;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Plugins.Responsive_UI.View
{
    public class PauseWindow : Window
    {
        [SerializeField] private Button _menuButton;

        [Inject] private PauseManager _pauseManager;

        private void Start()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        public override void Open()
        {
            _pauseManager.SetPaused(true);
        }

        public override void Close()
        {
            _pauseManager.SetPaused(false);
        }

        private void OnMenuButtonClicked()
        {
            Debug.Log("Go to menu");
        }
    }
}
