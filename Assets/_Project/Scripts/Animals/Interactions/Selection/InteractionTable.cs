using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Interaction Table", fileName = "InteractionTable")]
    public sealed class InteractionTable : ScriptableObject
    {
        [SerializeField]
        private List<InteractionRule> _rules = new();

        internal InteractionSelector CreateSelector() => new InteractionSelector(_rules);
    }
}
