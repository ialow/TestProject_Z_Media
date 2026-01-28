using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Entity/Specification", fileName = "EntitySpecification")]
    public class EntitySpecification : ScriptableObject
    {
        [SerializeField] public List<Specification> Specifications;
    }
}
