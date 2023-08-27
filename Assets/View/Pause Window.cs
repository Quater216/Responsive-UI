using System;
using Source;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class PauseWindow : MonoBehaviour, IWindow
    {
        public bool IsOpened { get; private set; }
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _menuButton;

        [Inject] private PauseManager _pauseManager;

        private void Start()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        public void Open()
        {
            _canvas.enabled = true;
            _pauseManager.SetPaused(true);
            IsOpened = true;
        }

        public void Close()
        {
            _canvas.enabled = false;
            _pauseManager.SetPaused(false);
            IsOpened = false;
        }

        private void OnMenuButtonClicked()
        {
            Debug.Log("Go to menu");
        }
    }
}
