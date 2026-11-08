namespace ZooWorld.Animals
{
    internal readonly struct Interaction
    {
        private readonly IInteractionStrategy _strategy;
        private readonly Animal _actor;
        private readonly Animal _target;

        public Interaction(IInteractionStrategy strategy, Animal actor, Animal target)
        {
            _strategy = strategy;
            _actor = actor;
            _target = target;
        }

        public void Apply() => _strategy.Apply(_actor, _target);
    }
}
