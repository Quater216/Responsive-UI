using UnityEngine;

namespace Plugins.Responsive_UI.View
{
    public class AnimatedWindow : Window
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int OpenAnimation = Animator.StringToHash("Open");
        private static readonly int CloseAnimation = Animator.StringToHash("Close");
        
        public override void Open()
        {
            base.Open();
            _animator.SetTrigger(OpenAnimation);
        }

        public override void Close()
        {
            base.Close();
            _animator.SetTrigger(CloseAnimation);
        }
    }
}
