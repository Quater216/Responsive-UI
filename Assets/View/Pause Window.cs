using Source;
using UnityEngine;
using Zenject;

namespace View
{
    public class PauseWindow : MonoBehaviour, IWindow
    {
        public bool IsOpened { get; private set; }
        
        [SerializeField] private Canvas _canvas;

        [Inject] private PauseManager _pauseManager;

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
    }
}
