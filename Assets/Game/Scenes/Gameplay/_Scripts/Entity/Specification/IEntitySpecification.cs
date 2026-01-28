namespace Game
{
    public interface IEntitySpecification
    {
        void Apply(Entity entity, ref EntityParameters parameters);
    }
}
