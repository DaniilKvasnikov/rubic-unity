using UnityEngine;

namespace Rubik
{
    [CreateAssetMenu(fileName = "ManhattanSettings", menuName = "HeuristicSettings/ManhattanSettings", order = 1)]
    public class ManhattanSettings : ScriptableObject
    {
        public float Cost = 0.1f;
    }
}