using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Spawn Table", fileName = "SpawnTable")]
    public sealed class SpawnTable : ScriptableObject
    {
        [SerializeField]
        private List<AnimalDefinition> _animals = new();
        public IReadOnlyList<AnimalDefinition> Animals => _animals;
    }
}
