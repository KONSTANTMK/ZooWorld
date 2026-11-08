using System;
using System.Numerics;

namespace ZooWorld.Animals
{
    public interface IAnimalView
    {
        event Action<IAnimalView> Collided;

        void Face(Vector2 direction);
        void SetHeight(float height);
        void SetSquash(float squash);
        void ShowLabel(string text, float seconds);
    }
}
