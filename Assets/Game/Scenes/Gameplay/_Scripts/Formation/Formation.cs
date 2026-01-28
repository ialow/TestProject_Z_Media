using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Formation
    {
        private readonly List<List<FormationEntity>> _gridEntities;
        private readonly IFormationBehaviour _formationBehaviour;

        public Formation(List<List<FormationEntity>> gridEntities, IFormationBehaviour formationBehaviour)
        {
            _gridEntities = gridEntities;
            _formationBehaviour = formationBehaviour;
        }

        public List<List<FormationEntity>> GetGridEntities() => _gridEntities;

        public bool IsAnyAlive()
        {
            for (int r = 0; r < _gridEntities.Count; r++)
            {
                for (int c = 0; c < _gridEntities[r].Count; c++)
                {
                    if (_gridEntities[r][c].Entity.HealthSystem.IsAlive)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(Formation enemyFormation, float timeScale)
        {
            for (int r = 0; r < _gridEntities.Count; r++)
            {
                for (int c = 0; c < _gridEntities[r].Count; c++)
                {
                    var entity = _gridEntities[r][c];
                    if (!entity.Entity.HealthSystem.IsAlive) continue;

                    entity.Task?.Update(timeScale);

                    if (entity.Task == null || entity.Task.IsComplete)
                    {
                        _formationBehaviour.Update(this, enemyFormation, r, c);
                    }
                }
            }
        }
    }
}
