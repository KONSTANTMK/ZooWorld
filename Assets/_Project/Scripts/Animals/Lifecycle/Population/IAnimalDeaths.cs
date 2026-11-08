using R3;

namespace ZooWorld.Animals
{
    public interface IAnimalDeaths
    {
        Observable<AnimalRole> Died { get; }
    }
}
