using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/Specification/Mesh", fileName = "MeshSpecification")]
    public class MeshSpecification : Specification
    {
        [SerializeField] private EntityModel _entityModel;
        [SerializeField] private float _additionalHP;
        [SerializeField] private float _additionalDamage;

        public override void Apply(Entity entity, ref EntityParameters parameters)
        {
            var instance = Instantiate(_entityModel, entity.transform);
            entity.SetModel3D(instance);

            parameters = new(parameters.HP + _additionalHP, parameters.Speed, parameters.Damage + _additionalDamage, parameters.Cooldown);
        }
    }
}
