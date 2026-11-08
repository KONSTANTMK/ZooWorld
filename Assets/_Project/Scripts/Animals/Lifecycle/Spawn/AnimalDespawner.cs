using System;
using System.Collections.Generic;
using R3;

namespace ZooWorld.Animals
{
    internal sealed class AnimalDespawner : IAnimalDespawner, IDisposable
    {
        private readonly AnimalPopulation _population;
        private readonly AnimalPool _pool;

        private readonly HashSet<PopulationMember> _removed = new();
        private readonly IDisposable _removalSubscription;

        public AnimalDespawner(AnimalPopulation population, AnimalPool pool)
        {
            _population = population;
            _pool = pool;
            _removalSubscription = population.MemberRemoved.Subscribe(OnMemberRemoved);
        }

        public void Despawn()
        {
            foreach (PopulationMember member in _removed)
            {
                Release(member);
            }

            _removed.Clear();
        }

        public void Dispose()
        {
            _removalSubscription.Dispose();
            IReadOnlyList<PopulationMember> active = _population.Active;
            for (var i = 0; i < active.Count; i++)
            {
                active[i].Presenter.Dispose();
            }
        }

        private void OnMemberRemoved(PopulationMember member) => _removed.Add(member);

        private void Release(PopulationMember member)
        {
            _population.Unregister(member);
            member.Presenter.Dispose();
            _pool.Release(member.View);
        }
    }
}
