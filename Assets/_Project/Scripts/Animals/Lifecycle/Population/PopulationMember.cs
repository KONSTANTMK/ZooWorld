using System;

namespace ZooWorld.Animals
{
    internal sealed class PopulationMember
    {
        public Animal Animal { get; }
        public AnimalView View { get; }
        public IDisposable Presenter { get; }

        public PopulationMember(Animal animal, AnimalView view, IDisposable presenter)
        {
            Animal = animal;
            View = view;
            Presenter = presenter;
        }
    }
}
