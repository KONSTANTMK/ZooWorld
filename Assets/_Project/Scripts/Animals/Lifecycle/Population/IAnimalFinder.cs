namespace ZooWorld.Animals
{
    internal interface IAnimalFinder
    {
        bool TryFind(IAnimalView view, out Animal animal);
    }
}
