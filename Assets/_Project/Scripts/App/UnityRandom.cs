using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.App
{
    internal sealed class UnityRandom : IRandom
    {
        public float Range(float min, float max) => Random.Range(min, max);
    }
}
