using System;

namespace Game
{
    public class HealthSystem
    {
        public float CurrentHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        public event Action<float> OnTakeDamage;
        public event Action OnDeath;

        public HealthSystem(float health)
        {
            CurrentHealth = health;
        }

        public void TakeDamage(float amount)
        {
            var possibleHealth = CurrentHealth - amount;

            if (possibleHealth > 0)
            {
                CurrentHealth = possibleHealth;
                OnTakeDamage?.Invoke(amount);
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
            CurrentHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
