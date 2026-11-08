using NUnit.Framework;
using ZooWorld.Animals;
using ZooWorld.UI;

namespace ZooWorld.Tests
{
    public sealed class UITests
    {
        [Test]
        public void Given_Deaths_When_Reported_Then_CounterShowsDeadPreysAndPredators()
        {
            var population = new FakePopulation();
            var view = new FakeCounterView();
            using var model = new DeathCounter(population);
            using var presenter = new DeathCounterPresenter(model, view);

            population.RaiseDeath(AnimalRole.Prey);
            population.RaiseDeath(AnimalRole.Prey);
            population.RaiseDeath(AnimalRole.Predator);

            Assert.That(view.Counts[AnimalRole.Prey], Is.EqualTo(2));
            Assert.That(view.Counts[AnimalRole.Predator], Is.EqualTo(1));
        }
    }
}
