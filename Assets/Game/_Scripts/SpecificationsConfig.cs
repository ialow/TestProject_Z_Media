using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/AllSpecification", fileName = "AllSpecification")]
    public class SpecificationsConfig : ScriptableObject
    {
        [SerializeField] private List<EntitySpecification> _specifications;

        public int Count => _specifications.Count;

        public EntitySpecification GetSpecification(int index)
        {
            return _specifications[index];
        }

        public EntitySpecification GetRandomSpecification()
        {
            return _specifications[Random.Range(0, Count)];
        }
    }
}