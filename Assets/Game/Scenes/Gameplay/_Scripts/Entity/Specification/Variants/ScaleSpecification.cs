using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/Specification/Scale", fileName = "ScaleSpecification")]
    public class ScaleSpecification : Specification
    {
        [SerializeField] private float _scale;
        [SerializeField] private float _additionalHP;

        public override void Apply(Entity entity, ref EntityParameters parameters)
        {
            entity.Model.SetScale(Vector3.one * _scale);
            entity.Model.SetLocalPosition(new(0f, 0.5f * _scale, 0f));
            parameters = new(parameters.HP + _additionalHP, parameters.Speed, parameters.Damage, parameters.Cooldown);
        }
    }
}
