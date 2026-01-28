namespace Game
{
    public class FormationEntity
    {
        public Entity Entity { get; private set; }
        public IEntityTask Task { get; private set; } = null;

        public FormationEntity(Entity entity)
        {
            Entity = entity;
        }

        public void SetTask(IEntityTask task)
        {
            Task?.End();
            Task = task;
            Task.Start();
        }
    }
}
