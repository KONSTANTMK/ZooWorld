using System;
using R3;

namespace ZooWorld.Animals
{
    internal sealed class AnimalPresenter : IDisposable
    {
        private readonly Animal _animal;
        private readonly IAnimalView _view;
        private readonly ICollisionQueue _collisionQueue;
        private readonly AnimalLabel _fedLabel;

        private readonly CompositeDisposable _subscriptions = new();

        public AnimalPresenter(
            Animal animal,
            IAnimalView view,
            ICollisionQueue collisionQueue,
            AnimalLabel fedLabel)
        {
            _animal = animal;
            _view = view;
            _collisionQueue = collisionQueue;
            _fedLabel = fedLabel;
            _view.Collided += OnCollided;

            _animal.Direction.Subscribe(_view.Face).AddTo(_subscriptions);
            _animal.Height.Subscribe(_view.SetHeight).AddTo(_subscriptions);
            _animal.Squash.Subscribe(_view.SetSquash).AddTo(_subscriptions);
            _animal.Fed.Subscribe(this, (_, presenter) => presenter.OnFed()).AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _view.Collided -= OnCollided;
            _subscriptions.Dispose();
        }

        private void OnCollided(IAnimalView other) => _collisionQueue.Enqueue(_view, other);

        private void OnFed() => _view.ShowLabel(_fedLabel.Text, _fedLabel.Seconds);
    }
}
