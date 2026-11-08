namespace ZooWorld.Animals
{
    internal sealed class DuelInteractionStrategy : IInteractionStrategy
    {
        private readonly EatInteractionStrategy _eat;

        public DuelInteractionStrategy(EatInteractionStrategy eat) => _eat = eat;

        public void Apply(Animal actor, Animal target)
        {
            bool isActorOlder = actor.Id < target.Id;
            Animal winner = isActorOlder ? actor : target;
            Animal loser = isActorOlder ? target : actor;

            _eat.Apply(winner, loser);
        }
    }
}
