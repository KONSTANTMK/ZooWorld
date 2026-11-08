using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Spawn Settings", fileName = "SpawnSettings")]
    public sealed class SpawnSettings : ScriptableObject
    {
        [SerializeField]
        [Min(0f)]
        private float _minInterval = 1f;
        public float MinInterval => _minInterval;

        [SerializeField]
        [Min(0f)]
        private float _maxInterval = 2f;
        public float MaxInterval => _maxInterval;

        [SerializeField]
        [Min(0f)]
        private float _freeRadius = 1.2f;
        public float FreeRadius => _freeRadius;

        [SerializeField]
        [Min(1)]
        private int _maxAttempts = 10;
        public int MaxAttempts => _maxAttempts;

        [SerializeField]
        [Min(0f)]
        private float _edgeMargin = 2f;
        public float EdgeMargin => _edgeMargin;

        [SerializeField]
        private LayerMask _animalLayers;
        public LayerMask AnimalLayers => _animalLayers;

        [SerializeField]
        [Min(1)]
        private int _maxAnimals = 60;
        public int MaxAnimals => _maxAnimals;
    }
}
