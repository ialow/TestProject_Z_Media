namespace Game
{
    public struct EntityParameters
    {
        public readonly float HP;
        public readonly float Speed;
        public readonly float Damage;
        public readonly float Cooldown;

        public EntityParameters(float hp, float speed, float damage, float cooldown)
        {
            HP = hp;
            Speed = speed;
            Damage = damage;
            Cooldown = cooldown;
        }
    }
}
