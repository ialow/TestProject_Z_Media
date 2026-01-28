using UnityEngine;

namespace Game
{
    public class AttackSystem
    {
        private readonly Entity _entity;
        private readonly float _damage;
        private readonly float _cooldown;
        private bool _isPerformingAttack;

        private Entity _currentTarget;
        private float _lastHitTimer = 0;

        public bool CanStartAttack => _lastHitTimer <= 0 && !_isPerformingAttack;

        public AttackSystem(Entity entity, float damage, float cooldown) 
        {
            _entity = entity;

            _damage = damage;
            _cooldown = cooldown;
        }

        public void Update(Entity target, float timeScale)
        {
            if (_lastHitTimer > 0 && !_isPerformingAttack)
                _lastHitTimer -= Time.deltaTime * timeScale;

            if (CanStartAttack && target != null)
            {
                _isPerformingAttack = true;
                _currentTarget = target;
                _entity.PlayAttackAnimation();
            }
        }

        public void ApplyAttack()
        {
            _currentTarget.HealthSystem.TakeDamage(_damage);
        }

        public void AttackEnded()
        {
            _isPerformingAttack = false;
            _lastHitTimer = _cooldown;
            _currentTarget = null;
        }
    }
}
