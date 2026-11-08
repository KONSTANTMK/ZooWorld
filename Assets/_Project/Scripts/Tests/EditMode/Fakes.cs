using System;
using System.Collections.Generic;
using System.Numerics;
using R3;
using ZooWorld.Animals;
using ZooWorld.Shared;
using ZooWorld.UI;

namespace ZooWorld.Tests
{
    internal sealed class FakeBody : IAnimalBodyComponent
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 LastImpulse { get; private set; }
        public bool AreCollisionsEnabled { get; private set; } = true;

        public void SetVelocity(Vector2 velocity) => Velocity = velocity;
        public void AddImpulse(Vector2 impulse) => LastImpulse = impulse;

        public void DisableCollisions() => AreCollisionsEnabled = false;

        public void Integrate(float deltaTime) => Position += Velocity * deltaTime;
    }

    internal sealed class FakeBounds : IScreenBounds
    {
        public const float HalfSize = 100f;

        public Vector2 Min { get; } = new(-HalfSize, -HalfSize);
        public Vector2 Max { get; } = new(HalfSize, HalfSize);
    }

    internal sealed class FakeRandom : IRandom
    {
        public float Value { get; set; }

        public float Range(float min, float max) => min + (max - min) * Value;
    }

    internal sealed class FakePopulation : IAnimalDeaths, IAnimalFinder
    {
        private readonly Subject<AnimalRole> _died = new();

        public Dictionary<IAnimalView, Animal> Animals { get; } = new();
        public Observable<AnimalRole> Died => _died;

        public bool TryFind(IAnimalView view, out Animal animal) => Animals.TryGetValue(view, out animal);

        public void RaiseDeath(AnimalRole role) => _died.OnNext(role);
    }

    internal sealed class FakeView : IAnimalView
    {
        public Vector2 FacedDirection { get; private set; }
        public float Height { get; private set; }
        public float Squash { get; private set; }
        public string ShownLabel { get; private set; }

        public event Action<IAnimalView> Collided;

        public void Collide(IAnimalView other) => Collided?.Invoke(other);
        public void ShowLabel(string text, float seconds) => ShownLabel = text;
        public void Face(Vector2 direction) => FacedDirection = direction;
        public void SetHeight(float height) => Height = height;
        public void SetSquash(float squash) => Squash = squash;
    }

    internal sealed class FakeCounterView : IDeathCounterView
    {
        public Dictionary<AnimalRole, int> Counts { get; } = new();
        public IReadOnlyList<AnimalRole> GetRoles() => new[] { AnimalRole.Prey, AnimalRole.Predator };

        public void SetCount(AnimalRole role, int count) => Counts[role] = count;
    }
}
