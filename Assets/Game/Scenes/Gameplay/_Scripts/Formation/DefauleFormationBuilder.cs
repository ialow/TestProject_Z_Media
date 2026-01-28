using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DefauleFormationBuilder : IFormationBuilder
    {
        private readonly int _entitiesCount;
        private readonly int _maxEntitiesInRow;
        private readonly float _offset;

        private readonly Vector3 _origin;

        public DefauleFormationBuilder(int entitiesCount, int maxEntitiesInRow, float offset, Vector3 origin)
        {
            _entitiesCount = entitiesCount;
            _maxEntitiesInRow = maxEntitiesInRow;
            _offset = offset;
            _origin = origin;
        }

        public List<List<Vector3>> CreateGrid()
        {
            var rows = new List<List<Vector3>>();
            int remainingEntities = _entitiesCount;
            int currentRowIndex = 0;

            while (remainingEntities > 0)
            {
                int entitiesInThisRow = Mathf.Min(_maxEntitiesInRow, remainingEntities);
                var rowPositions = new List<Vector3>(entitiesInThisRow);

                for (int col = 0; col < entitiesInThisRow; col++)
                {
                    float xOffset = (col - (entitiesInThisRow - 1) * 0.5f) * _offset;
                    float zOffset = -currentRowIndex * _offset;

                    Vector3 position = _origin + new Vector3(xOffset, 0, zOffset);
                    rowPositions.Add(position);
                }

                rows.Add(rowPositions);
                remainingEntities -= entitiesInThisRow;
                currentRowIndex++;
            }

            return rows;
        }
    }
}