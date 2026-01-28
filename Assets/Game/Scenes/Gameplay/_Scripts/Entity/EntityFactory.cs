using UnityEngine;

namespace Game
{
    public class EntityFactory
    {
        private readonly Entity _coreEntity;
        private readonly EntityData _baseEntityData;

        public EntityFactory(Entity coreEntity, EntityData baseEntityData)
        {
            _coreEntity = coreEntity;
            _baseEntityData = baseEntityData;
        }

        public Entity Create(Vector3 position, Transform parent, EntitySpecification specifications)
        {
            Entity entity = UnityEngine.Object.Instantiate(_coreEntity, position, parent.rotation, parent);
            var parameters = new EntityParameters(_baseEntityData.HP, _baseEntityData.Speed, _baseEntityData.Damage, _baseEntityData.Cooldown);

            specifications.Specifications.ForEach(spec => spec.Apply(entity, ref parameters));
            entity.SetParameters(parameters);

            return entity;
        }
    }
}
