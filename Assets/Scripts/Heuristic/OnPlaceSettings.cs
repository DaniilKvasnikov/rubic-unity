using UnityEngine;

namespace Rubik
{
    [CreateAssetMenu(fileName = "OnPlaceSettings", menuName = "HeuristicSettings/OnPlaceSettings", order = 1)]
    public class OnPlaceSettings : ScriptableObject
    {
        public float Cost = 20f;
    }
}