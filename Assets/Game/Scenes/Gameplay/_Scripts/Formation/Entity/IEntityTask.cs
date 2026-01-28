namespace Game
{
    public interface IEntityTask
    {
        bool IsComplete { get; }
        EntityState State { get; }

        void Start();
        void Update(float timeScale);
        void End();
    }
}