using NUnit.Framework;
using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    public sealed class AnimalsTests
    {
        private const string FrogId = "frog";
        private const string SnakeId = "snake";
        private const float JumpDistance = 2f;
        private const float JumpInterval = 1f;
        private const float FlightTime = 0.4f;
        private const float JumpHeight = 0.6f;
        private const float LandingMargin = 0.2f;
        private const float MiddleRoll = 0.5f;
        private const int JumpCount = 2;
        private const float SnakeSpeed = 3f;
        private const float TurnInterval = 10f;
        private const float OneSecond = 1f;
        private const float DistanceTolerance = 0.1f;

        [Test]
        public void Given_FrogCard_When_Loaded_Then_FrogIsPrey()
        {
            Assert.That(TestContent.LoadAnimal(FrogId).Role, Is.EqualTo(AnimalRole.Prey));
        }

        [Test]
        public void Given_FrogCard_When_Loaded_Then_FrogMovesByJumps()
        {
            Assert.That(TestContent.LoadAnimal(FrogId).Movement, Is.InstanceOf<JumpMovementConfig>());
        }

        [Test]
        public void Given_Frog_When_JumpIntervalPasses_Then_ItJumpsAgain()
        {
            MovementRig rig = CreateFrogRig();

            rig.Simulate(seconds: JumpCount * JumpInterval + LandingMargin);

            float expected = JumpCount * JumpDistance;
            Assert.That(rig.Body.Position.Length(), Is.EqualTo(expected).Within(JumpCount * DistanceTolerance));
        }

        [Test]
        public void Given_Frog_When_JumpEnds_Then_ItTravelledFixedDistance()
        {
            MovementRig rig = CreateFrogRig();

            rig.Simulate(seconds: JumpInterval + LandingMargin);

            Assert.That(rig.Body.Position.Length(), Is.EqualTo(JumpDistance).Within(DistanceTolerance));
        }

        [Test]
        public void Given_SnakeCard_When_Loaded_Then_SnakeIsPredator()
        {
            Assert.That(TestContent.LoadAnimal(SnakeId).Role, Is.EqualTo(AnimalRole.Predator));
        }

        [Test]
        public void Given_Snake_When_OneSecondPasses_Then_ItTravelledFixedDistance()
        {
            var rig = new MovementRig(new LinearMovementStrategy(SnakeSpeed, TurnInterval), new FakeRandom());

            rig.Simulate(seconds: OneSecond);

            Assert.That(rig.Body.Position.Length(), Is.EqualTo(SnakeSpeed * OneSecond).Within(DistanceTolerance));
        }

        private static MovementRig CreateFrogRig()
        {
            var jump = new JumpMovementStrategy(JumpDistance, JumpInterval, FlightTime, JumpHeight);
            return new MovementRig(jump, new FakeRandom { Value = MiddleRoll });
        }
    }
}
