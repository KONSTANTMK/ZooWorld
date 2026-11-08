using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Animals
{
    internal sealed class CollisionQueue : ICollisionQueue
    {
        private readonly InteractionSelector _selector;
        private readonly IAnimalFinder _finder;

        private readonly Queue<(IAnimalView First, IAnimalView Second)> _pending = new();
        private readonly HashSet<(int LowId, int HighId)> _seenPairs = new();

        public CollisionQueue(InteractionSelector selector, IAnimalFinder finder)
        {
            _selector = selector;
            _finder = finder;
        }

        public void Enqueue(IAnimalView first, IAnimalView second) => _pending.Enqueue((first, second));

        public void DequeueAll()
        {
            while (_pending.TryDequeue(out (IAnimalView First, IAnimalView Second) pair))
            {
                ApplySafely(pair.First, pair.Second);
            }

            _seenPairs.Clear();
        }

        private void ApplySafely(IAnimalView firstView, IAnimalView secondView)
        {
            try
            {
                Apply(firstView, secondView);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private void Apply(IAnimalView firstView, IAnimalView secondView)
        {
            if (!_finder.TryFind(firstView, out Animal first) || !_finder.TryFind(secondView, out Animal second))
            {
                return;
            }

            if (!first.IsAlive || !second.IsAlive)
            {
                return;
            }

            if (!_seenPairs.Add(GetPairKey(first, second)))
            {
                return;
            }

            if (_selector.TrySelect(first, second, out Interaction interaction))
            {
                interaction.Apply();
            }
        }

        private static (int LowId, int HighId) GetPairKey(Animal first, Animal second)
        {
            return first.Id < second.Id ? (first.Id, second.Id) : (second.Id, first.Id);
        }
    }
}
