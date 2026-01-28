using UnityEngine;

namespace Game
{
    public class World : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerTeamOrign { get; private set; }
        [field: SerializeField] public Transform EnemyTeamOrign { get; private set; }
    }
}
