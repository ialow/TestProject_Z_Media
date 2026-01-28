using UnityEngine;

namespace Game
{
    public class MoveToTask : IEntityTask
    {
        private readonly FormationEntity _formationEntity;
        private readonly Transform _targetTransform;
        private Vector3 _lastPathPosition;

        private bool _isComplete = false;
        public bool IsComplete => _isComplete;
        public EntityState State => EntityState.Move;

        public MoveToTask(FormationEntity formationEntity, Transform targetTransform)
        {
            _formationEntity = formationEntity;
            _targetTransform = targetTransform;
        }

        public void Start()
        {
            if (_targetTransform == null) return;

            UpdatePath();
        }

        public void Update(float timeScale)
        {
            if (_isComplete || _targetTransform == null) return;

            var agent = _formationEntity.Entity.Agent;
            agent.speed = _formationEntity.Entity.Parameters.Speed * timeScale;

            if (Vector3.Distance(_targetTransform.position, _lastPathPosition) > 4.0f)
            {
                UpdatePath();
            }

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                End();
            }
        }

        private void UpdatePath()
        {
            _lastPathPosition = _targetTransform.position;
            _formationEntity.Entity.Agent.SetDestination(_lastPathPosition);
        }

        public void End()
        {
            _isComplete = true;

            _formationEntity.Entity.Agent.isStopped = true;
            
            if(_formationEntity.Entity.Agent.isOnNavMesh)
                _formationEntity.Entity.Agent.ResetPath();
        }
    }
}