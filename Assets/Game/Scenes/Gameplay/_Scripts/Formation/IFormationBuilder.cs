using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IFormationBuilder
    {
        List<List<Vector3>> CreateGrid();
    }
}