using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        
        public EntityParameters Parameters { get; private set; }
        public HealthSystem HealthSystem { get; private set; }
        public AttackSystem AttackSystem { get; private set; }

        public EntityModel Model { get; private set; }
        public FormationEntity FormationEntity { get; private set; }

        public void SetModel3D(EntityModel model) => Model = model;
        public void SetParameters(EntityParameters parameters) => Parameters = parameters;

        public void Init(FormationEntity formationEntity)
        {
            FormationEntity = formationEntity;
            HealthSystem = new HealthSystem(Parameters.HP);
            AttackSystem = new AttackSystem(this, Parameters.Damage, Parameters.Cooldown);

            Agent.speed = Parameters.Speed;

            HealthSystem.OnDeath += Die;
            HealthSystem.OnTakeDamage += GetDamage;
        }

        public void Deinit()
        {
            HealthSystem.OnDeath -= Die;
            HealthSystem.OnTakeDamage -= GetDamage;
        }

        public void LookAt(Vector3 targetPosition)
        {
            var direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        public void PlayAttackAnimation()
        {
            OnAnimationHit(); //TODO: Animation events...
            OnAnimationAttackEnded(); //TODO: Animation events...
        }

        public void OnAnimationHit()
        {
            AttackSystem.ApplyAttack();
        }

        public void OnAnimationAttackEnded()
        {
            AttackSystem.AttackEnded();
        }

        private void Die()
        {
            Deinit();
            gameObject.SetActive(false);
        }

        private void GetDamage(float damage)
        {
            
        }
    }
}
