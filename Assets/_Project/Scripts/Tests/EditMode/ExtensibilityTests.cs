using System.Collections.Generic;
using NUnit.Framework;
using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    public sealed class ExtensibilityTests
    {
        [TestCaseSource(nameof(LoadAllAnimals))]
        public void Given_AnimalCard_When_Validated_Then_ItHasNoErrors(AnimalDefinition animal)
        {
            Assert.That(AnimalDefinitionValidator.FindErrors(animal), Is.Empty);
        }

        private static IEnumerable<AnimalDefinition> LoadAllAnimals() => TestContent.LoadAll<AnimalDefinition>();
    }
}
