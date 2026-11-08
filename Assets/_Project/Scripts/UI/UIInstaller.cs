using Zenject;

namespace ZooWorld.UI
{
    public sealed class UIInstaller : Installer<DeathCounterView, UIInstaller>
    {
        private readonly DeathCounterView _view;

        public UIInstaller(DeathCounterView view) => _view = view;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DeathCounter>().AsSingle();
            Container.BindInstance<IDeathCounterView>(_view);
            Container.BindInterfacesTo<DeathCounterPresenter>().AsSingle().NonLazy();
        }
    }
}
