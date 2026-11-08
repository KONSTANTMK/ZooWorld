using System;
using System.Collections.Generic;
using R3;
using ZooWorld.Animals;

namespace ZooWorld.UI
{
    internal sealed class DeathCounter : IDisposable
    {
        private readonly Dictionary<AnimalRole, ReactiveProperty<int>> _counts = new();
        private readonly IDisposable _deathSubscription;

        public DeathCounter(IAnimalDeaths deaths)
        {
            _deathSubscription = deaths.Died.Subscribe(this, (role, counter) => counter.OnDied(role));
        }

        public ReadOnlyReactiveProperty<int> ObserveCount(AnimalRole role) => GetOrCreateCounter(role);

        public void Dispose()
        {
            _deathSubscription.Dispose();
            foreach (ReactiveProperty<int> count in _counts.Values)
            {
                count.Dispose();
            }
        }

        private void OnDied(AnimalRole role) => GetOrCreateCounter(role).Value++;

        private ReactiveProperty<int> GetOrCreateCounter(AnimalRole role)
        {
            if (!_counts.TryGetValue(role, out ReactiveProperty<int> counter))
            {
                counter = new();
                _counts.Add(role, counter);
            }

            return counter;
        }
    }
}
