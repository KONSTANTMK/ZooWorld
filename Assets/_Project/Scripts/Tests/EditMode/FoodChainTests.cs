using System;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using R3;
using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    public sealed class FoodChainTests
    {
        private const string TastyLabel = "Tasty!";
        private const float PreyGap = 1f;

        private FakePopulation _population;
        private CollisionQueue _collisionQueue;

        [SetUp]
        public void SetUp()
        {
            _population = new FakePopulation();
            InteractionSelector selector = TestContent.LoadSingle<InteractionTable>().CreateSelector();
            _collisionQueue = new CollisionQueue(selector, _population);
        }

        [Test]
        public void Given_TwoPrey_When_Collided_Then_TheyFlyApart()
        {
            var firstBody = new FakeBody();
            var secondBody = new FakeBody { Position = new Vector2(PreyGap, 0f) };
            Animal first = TestAnimals.Create(AnimalRole.Prey, firstBody);
            Animal second = TestAnimals.Create(AnimalRole.Prey, secondBody);

            Collide(first, second);

            Assert.That(first.IsAlive, Is.True);
            Assert.That(second.IsAlive, Is.True);
            Assert.That(firstBody.LastImpulse.X, Is.LessThan(0f));
            Assert.That(secondBody.LastImpulse.X, Is.GreaterThan(0f));
        }

        [Test]
        public void Given_PreyAndPredator_When_Collided_Then_PreyDies()
        {
            Animal prey = TestAnimals.Create(AnimalRole.Prey);

            Collide(prey, TestAnimals.Create(AnimalRole.Predator));

            Assert.That(prey.IsAlive, Is.False);
        }

        [Test]
        public void Given_PredatorAndPrey_When_Collided_Then_PredatorEatsIt()
        {
            Animal predator = TestAnimals.Create(AnimalRole.Predator);
            int meals = 0;
            using IDisposable subscription = predator.Fed.Subscribe(_ => meals++);

            Collide(predator, TestAnimals.Create(AnimalRole.Prey));

            Assert.That(predator.IsAlive, Is.True);
            Assert.That(meals, Is.EqualTo(1));
        }

        [Test]
        public void Given_TwoPredators_When_Collided_Then_OnlyOneSurvives()
        {
            Animal[] predators = { TestAnimals.Create(AnimalRole.Predator), TestAnimals.Create(AnimalRole.Predator) };

            Collide(predators[0], predators[1]);

            Assert.That(predators.Count(predator => predator.IsAlive), Is.EqualTo(1));
        }

        [Test]
        public void Given_PresentedPredator_When_ItEats_Then_TastyLabelIsShown()
        {
            Animal predator = TestAnimals.Create(AnimalRole.Predator);
            FakeView predatorView = Register(predator);
            AnimalLabel label = TestContent.LoadSingle<AnimalLabel>();
            using var presenter = new AnimalPresenter(predator, predatorView, _collisionQueue, label);

            predatorView.Collide(Register(TestAnimals.Create(AnimalRole.Prey)));
            _collisionQueue.DequeueAll();

            Assert.That(predatorView.ShownLabel, Is.EqualTo(TastyLabel));
        }

        private void Collide(Animal first, Animal second)
        {
            _collisionQueue.Enqueue(Register(first), Register(second));
            _collisionQueue.DequeueAll();
        }

        private FakeView Register(Animal animal)
        {
            var view = new FakeView();
            _population.Animals.Add(view, animal);
            return view;
        }
    }
}
