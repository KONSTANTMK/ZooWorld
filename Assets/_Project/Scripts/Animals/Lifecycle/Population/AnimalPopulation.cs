using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace ZooWorld.Animals
{
    internal sealed class AnimalPopulation : IAnimalPopulation, IAnimalDeaths, IAnimalFinder, IDisposable
    {
        private readonly List<PopulationMember> _active = new();
        private readonly Dictionary<IAnimalView, PopulationMember> _byView = new();
        private readonly Dictionary<PopulationMember, IDisposable> _deathSubscriptions = new();

        private readonly Subject<AnimalRole> _died = new();
        private readonly Subject<PopulationMember> _memberRemoved = new();

        public IReadOnlyList<PopulationMember> Active => _active;
        public int Count => _active.Count;
        public Observable<AnimalRole> Died => _died;
        public Observable<PopulationMember> MemberRemoved => _memberRemoved;

        public void Register(Animal animal, AnimalView view, IDisposable presenter)
        {
            var member = new PopulationMember(animal, view, presenter);
            _active.Add(member);
            _byView.Add(view, member);
            IDisposable subscription = animal.Died.Subscribe(_ => OnDied(member));
            _deathSubscriptions.Add(member, subscription);
        }

        public void Unregister(PopulationMember member)
        {
            _active.Remove(member);
            _byView.Remove(member.View);
            _deathSubscriptions[member].Dispose();
            _deathSubscriptions.Remove(member);
        }

        public bool TryFind(IAnimalView view, out Animal animal)
        {
            bool isFound = _byView.TryGetValue(view, out PopulationMember member);
            animal = isFound ? member.Animal : null;
            return isFound;
        }

        public void Tick(float deltaTime)
        {
            for (var i = 0; i < _active.Count; i++)
            {
                TickSafely(_active[i], deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable subscription in _deathSubscriptions.Values)
            {
                subscription.Dispose();
            }

            _died.Dispose();
            _memberRemoved.Dispose();
        }

        private void TickSafely(PopulationMember member, float deltaTime)
        {
            try
            {
                member.Animal.Tick(deltaTime);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception, member.View);
                Remove(member);
            }
        }

        private void OnDied(PopulationMember member)
        {
            _died.OnNext(member.Animal.Role);
            Remove(member);
        }

        private void Remove(PopulationMember member)
        {
            _byView.Remove(member.View);
            _memberRemoved.OnNext(member);
        }
    }
}
