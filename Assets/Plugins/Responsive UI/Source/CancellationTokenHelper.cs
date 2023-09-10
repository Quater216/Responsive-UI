using System;
using System.Threading;
using UnityEditor;

namespace Plugins.Responsive_UI.Source
{
    public class CancellationTokenHelper : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposed;

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        public CancellationTokenHelper()
        {
            _cancellationTokenSource = new CancellationTokenSource();
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingPlayMode)
            {
                _cancellationTokenSource?.Cancel();
            }
        }
#endif
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool triggeredByUserCode)
        {
            if (_disposed)
            {
                return;
            }
            
            if (triggeredByUserCode)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif

            _disposed = true;
        }

        ~CancellationTokenHelper()
        {
            Dispose(false);
        }
    }
}