using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/BaseParameters", fileName = "BaseParameters")]
    public class EntityData : ScriptableObject
    {
        public float HP;
        public float Speed;
        public float Damage;
        public float Cooldown;
    }
}
