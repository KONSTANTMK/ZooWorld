using System;
using System.Numerics;
using NUnit.Framework;
using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    public sealed class GameplayTests
    {
        private const float Speed = 1f;
        private const float TurnInterval = 0.5f;
        private const float AfterFirstTurn = 0.9f;
        private const float QuarterRoll = 0.25f;
        private const float OffScreenOffset = 50f;
        private const float DirectionTolerance = 0.01f;

        private static readonly float DiagonalComponent = MathF.Sqrt(0.5f);

        [Test]
        public void Given_MovingAnimal_When_TurnIntervalPasses_Then_ItTurnsToRandomDirection()
        {
            var random = new FakeRandom();
            var rig = new MovementRig(new LinearMovementStrategy(Speed, TurnInterval), random);
            random.Value = QuarterRoll;

            rig.Simulate(seconds: AfterFirstTurn);

            Vector2 direction = rig.Mover.Direction.CurrentValue;
            Assert.That(direction.X, Is.EqualTo(DiagonalComponent).Within(DirectionTolerance));
            Assert.That(direction.Y, Is.EqualTo(-DiagonalComponent).Within(DirectionTolerance));
        }

        [Test]
        public void Given_AnimalOutOfScreen_When_Ticked_Then_DirectionPointsBackToScreen()
        {
            var body = new FakeBody { Position = new Vector2(FakeBounds.HalfSize + OffScreenOffset, 0f) };
            var direction = new AnimalDirection(body, new FakeBounds(), new FakeRandom());
            direction.Redirect(Vector2.UnitX);

            direction.Tick(MovementRig.Step);

            Assert.That(direction.Current.CurrentValue.X, Is.LessThan(0f));
        }
    }
}
