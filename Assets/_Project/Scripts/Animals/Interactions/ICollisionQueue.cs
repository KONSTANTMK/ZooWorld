namespace ZooWorld.Animals
{
    public interface ICollisionQueue
    {
        void Enqueue(IAnimalView first, IAnimalView second);
        void DequeueAll();
    }
}
