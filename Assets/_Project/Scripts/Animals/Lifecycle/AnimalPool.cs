using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ZooWorld.Animals
{
    internal sealed class AnimalPool
    {
        private readonly Transform _root;

        private readonly Dictionary<AnimalDefinition, ObjectPool<AnimalView>> _pools = new();
        private readonly Dictionary<AnimalView, ObjectPool<AnimalView>> _owners = new();

        public AnimalPool(Transform root) => _root = root;

        public AnimalView Get(AnimalDefinition definition) => GetOrCreatePool(definition).Get();

        public void Release(AnimalView view) => _owners[view].Release(view);

        private ObjectPool<AnimalView> GetOrCreatePool(AnimalDefinition definition)
        {
            if (_pools.TryGetValue(definition, out ObjectPool<AnimalView> pool))
            {
                return pool;
            }

            pool = CreatePool(definition);
            _pools.Add(definition, pool);
            return pool;
        }

        private ObjectPool<AnimalView> CreatePool(AnimalDefinition definition)
        {
            return new ObjectPool<AnimalView>(
                () => Create(definition),
                view => view.Activate(),
                view => view.Deactivate(),
                Destroy);
        }

        private AnimalView Create(AnimalDefinition definition)
        {
            AnimalView view = Object.Instantiate(definition.ViewPrefab, _root);
            view.name = definition.name;
            _owners.Add(view, _pools[definition]);
            return view;
        }

        private void Destroy(AnimalView view)
        {
            _owners.Remove(view);
            Object.Destroy(view.gameObject);
        }
    }
}
