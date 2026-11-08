using System.Numerics;

namespace ZooWorld.Animals
{
    internal sealed class BounceInteractionStrategy : IInteractionStrategy
    {
        private readonly float _force;
        private readonly float _pushSeconds;

        public BounceInteractionStrategy(float force, float pushSeconds)
        {
            _force = force;
            _pushSeconds = pushSeconds;
        }

        public void Apply(Animal actor, Animal target)
        {
            Vector2 actorDirection = actor.GetDirectionFrom(target);
            Vector2 targetDirection = target.GetDirectionFrom(actor);
            Push(actor, actorDirection);
            Push(target, targetDirection);
        }

        private void Push(Animal animal, Vector2 direction) => animal.Push(direction, _force, _pushSeconds);
    }
}
