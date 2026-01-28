using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DefaultFormationBehaviour : IFormationBehaviour
    {
        public void Update(Formation thisFormation, Formation enemyFormation, int entityRow, int entityCol)
        {
            var grid = thisFormation.GetGridEntities();
            var entity = grid[entityRow][entityCol];
            var enemyTarget = FindTargetInFormation(entityRow, entityCol, grid, enemyFormation);

            if (enemyTarget == null) return;

            var distance = Vector3.Distance(entity.Entity.transform.position, enemyTarget.Entity.transform.position);
            var attackRange = entity.Entity.Agent.stoppingDistance + 0.2f;

            if (distance <= attackRange)
            {
                entity.SetTask(new AttackTask(entity, enemyTarget));
            }
            else
            {
                entity.SetTask(new MoveToTask(entity, enemyTarget.Entity.transform));
            }
        }

        private FormationEntity FindTargetInFormation(int myRow, int myCol, List<List<FormationEntity>> grid, Formation enemyFormation)
        {
            var enemyGrid = enemyFormation.GetGridEntities();

            for (int rowIndex = 0; rowIndex < enemyGrid.Count; rowIndex++)
            {
                var currentRow = enemyGrid[rowIndex];

                var ratio = (float)myCol / Mathf.Max(1, grid[myRow].Count - 1);
                var targetIndex = Mathf.RoundToInt(Mathf.Lerp(0, currentRow.Count - 1, ratio));

                FormationEntity bestTargetInRow = null;
                var checkOffsets = new int[]{ 0, -1, 1 }; 

                foreach (var offset in checkOffsets)
                {
                    var checkIdx = targetIndex + offset;
                    if (checkIdx >= 0 && checkIdx < currentRow.Count)
                    {
                        var potentialTarget = currentRow[checkIdx];
                        if (potentialTarget.Entity.HealthSystem.IsAlive)
                        {
                            bestTargetInRow = potentialTarget;
                            break;
                        }
                    }
                }

                if (bestTargetInRow != null)
                {
                    return bestTargetInRow;
                }
            }

            return FindGlobalClosestAlive(grid, enemyGrid);
        }

        private FormationEntity FindGlobalClosestAlive(List<List<FormationEntity>> grid, List<List<FormationEntity>> enemyGrid)
        {
            FormationEntity closest = null;
            var minDistance = Mathf.Infinity;
            var myPos = grid[0][0].Entity.transform.position;

            foreach (var row in enemyGrid)
            {
                foreach (var entity in row)
                {
                    if (entity.Entity.HealthSystem.IsAlive)
                    {
                        float dist = Vector3.Distance(myPos, entity.Entity.transform.position);
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            closest = entity;
                        }
                    }
                }
            }
            
            return closest;
        }
    }
}
