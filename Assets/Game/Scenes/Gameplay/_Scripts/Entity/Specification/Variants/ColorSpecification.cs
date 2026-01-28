using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/Specification/Color", fileName = "ColorSpecification")]
    public class ColorSpecification : Specification
    {
        [SerializeField] private Color _color;
        [SerializeField] private EntityData _additionalParameters;

        public override void Apply(Entity entity, ref EntityParameters parameters)
        {
            entity.Model.SetColor(_color);

            parameters = new(
                parameters.HP + _additionalParameters.HP, 
                parameters.Speed + _additionalParameters.Speed, 
                parameters.Damage + _additionalParameters.Damage, 
                parameters.Cooldown + _additionalParameters.Cooldown);
        }
    }
}
