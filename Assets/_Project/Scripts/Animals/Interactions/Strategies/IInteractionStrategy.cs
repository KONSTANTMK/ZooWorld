namespace ZooWorld.Animals
{
    internal interface IInteractionStrategy
    {
        void Apply(Animal actor, Animal target);
    }
}
