using UnityEngine;

namespace Game
{
    public class AttackTask : IEntityTask
    {
        private readonly FormationEntity _self;
        private readonly FormationEntity _target;
        private bool _isComplete = false;

        public bool IsComplete => _isComplete;
        public EntityState State => EntityState.Attack;

        public AttackTask(FormationEntity self, FormationEntity target)
        {
            _self = self;
            _target = target;
        }

        public void Start()
        {
            
        }

        public void Update(float timeScale)
        {
            if (_isComplete || _target == null || !_target.Entity.HealthSystem.IsAlive)
            {
                End();
                return;
            }
            
            _self.Entity.LookAt(_target.Entity.transform.position);
            _self.Entity.AttackSystem.Update(_target.Entity, timeScale);
        }

        public void End()
        {
            if (_isComplete) return;
            
            _isComplete = true;
        }
    }
}
