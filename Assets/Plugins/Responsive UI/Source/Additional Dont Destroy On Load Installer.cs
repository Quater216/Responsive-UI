using Zenject;

namespace Plugins.Responsive_UI.Source
{
    public class AdditionalDontDestroyOnLoadInstaller : MonoInstaller
    {
        private readonly PauseManager _pauseManager = new();

        public override void InstallBindings()
        {
            Container
                .Bind<PauseManager>()
                .FromInstance(_pauseManager)
                .AsSingle()
                .NonLazy();
        }
    }
}
