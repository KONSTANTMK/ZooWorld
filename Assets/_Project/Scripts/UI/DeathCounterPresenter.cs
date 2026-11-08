using System;
using R3;
using ZooWorld.Animals;

namespace ZooWorld.UI
{
    internal sealed class DeathCounterPresenter : IDisposable
    {
        private readonly IDeathCounterView _view;

        private readonly CompositeDisposable _subscriptions = new();

        public DeathCounterPresenter(DeathCounter model, IDeathCounterView view)
        {
            _view = view;
            foreach (AnimalRole role in view.GetRoles())
            {
                model.ObserveCount(role)
                    .Subscribe(count => _view.SetCount(role, count))
                    .AddTo(_subscriptions);
            }
        }

        public void Dispose() => _subscriptions.Dispose();
    }
}
