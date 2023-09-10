using UnityEngine;

namespace Plugins.Responsive_UI.View
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        public bool IsOpened { get; protected set; }

        public virtual void Open()
        {
            _canvas.enabled = true;
            IsOpened = true;
        }

        public virtual void Close()
        {
            _canvas.enabled = false;
            IsOpened = false;
        }
    }
}