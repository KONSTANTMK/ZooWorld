using System.Collections.Generic;
using ZooWorld.Animals;

namespace ZooWorld.UI
{
    internal interface IDeathCounterView
    {
        IReadOnlyList<AnimalRole> GetRoles();

        void SetCount(AnimalRole role, int count);
    }
}
