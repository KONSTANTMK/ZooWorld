using System.Collections.Generic;
using System.Linq;

namespace ZooWorld.Animals
{
    internal sealed class InteractionSelector
    {
        private readonly InteractionRule[] _rules;
        private readonly IInteractionStrategy[] _strategies;

        public InteractionSelector(IEnumerable<InteractionRule> rules)
        {
            _rules = rules.ToArray();
            _strategies = _rules.Select(rule => rule.CreateStrategy()).ToArray();
        }

        public bool TrySelect(Animal first, Animal second, out Interaction interaction)
        {
            for (var i = 0; i < _rules.Length; i++)
            {
                if (_rules[i].HasRoles(first.Role, second.Role))
                {
                    interaction = new Interaction(_strategies[i], first, second);
                    return true;
                }

                if (_rules[i].HasRoles(second.Role, first.Role))
                {
                    interaction = new Interaction(_strategies[i], second, first);
                    return true;
                }
            }

            interaction = default;
            return false;
        }
    }
}
