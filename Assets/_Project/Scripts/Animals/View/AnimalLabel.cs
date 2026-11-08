using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Animal Label", fileName = "AnimalLabel")]
    public sealed class AnimalLabel : ScriptableObject
    {
        [SerializeField]
        private string _text;
        public string Text => _text;

        [SerializeField]
        [Min(0f)]
        private float _seconds = 1f;
        public float Seconds => _seconds;
    }
}
