using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Animal", fileName = "Animal")]
    public sealed class AnimalDefinition : ScriptableObject
    {
        [SerializeField]
        private string _id;
        public string Id => _id;

        [SerializeField]
        private float _spawnWeight = 1f;
        public float SpawnWeight => _spawnWeight;

        [SerializeField]
        private AnimalRole _role;
        public AnimalRole Role => _role;

        [SerializeField]
        private AnimalView _viewPrefab;
        public AnimalView ViewPrefab => _viewPrefab;

        [SerializeField]
        private MovementConfig _movement;
        public MovementConfig Movement => _movement;
    }
}
