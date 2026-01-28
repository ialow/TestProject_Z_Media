using UnityEngine;

namespace Game
{
    public abstract class Specification : ScriptableObject, IEntitySpecification
    {
        public abstract void Apply(Entity entity, ref EntityParameters parameters);
    }
}
