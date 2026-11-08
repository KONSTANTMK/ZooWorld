using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.App
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Screen Settings", fileName = "ScreenSettings")]
    public sealed class ScreenSettings : ScriptableObject
    {
        [SerializeField]
        [Min(0f)]
        private float _margin = 1.2f;
        public float Margin => _margin;
    }
}
