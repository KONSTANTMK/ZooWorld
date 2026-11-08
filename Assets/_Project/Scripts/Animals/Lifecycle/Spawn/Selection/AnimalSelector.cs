using System;
using System.Linq;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class AnimalSelector
    {
        private readonly IRandom _random;

        private readonly AnimalDefinition[] _animals;
        private readonly float[] _weights;
        private readonly float _totalWeight;

        public AnimalSelector(SpawnTable table, IRandom random)
        {
            if (table.Animals.Count == 0)
            {
                throw new ArgumentException("Spawn table is empty.", nameof(table));
            }

            _random = random;
            _animals = table.Animals.ToArray();
            _weights = _animals.Select(animal => animal.SpawnWeight).ToArray();
            _totalWeight = _weights.Sum();
        }

        public AnimalDefinition Select() => _animals[SelectIndex()];

        private int SelectIndex()
        {
            float roll = _random.Range(0f, _totalWeight);
            for (var i = 0; i < _weights.Length; i++)
            {
                roll -= _weights[i];
                if (roll < 0f)
                {
                    return i;
                }
            }

            return _weights.Length - 1;
        }
    }
}
