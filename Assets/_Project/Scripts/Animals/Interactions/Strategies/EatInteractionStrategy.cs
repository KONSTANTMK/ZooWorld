namespace ZooWorld.Animals
{
    internal sealed class EatInteractionStrategy : IInteractionStrategy
    {
        public void Apply(Animal actor, Animal target)
        {
            actor.Feed();
            target.Die();
        }
    }
}
