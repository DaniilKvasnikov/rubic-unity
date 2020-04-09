using UnityEngine;

namespace Rubik
{
    [CreateAssetMenu(fileName = "HeuristicSettings", menuName = "HeuristicSettings/SettingContainer", order = 1)]
    public class HeuristicSettings : ScriptableObject
    {
        public HeuristicType heuristicType;
        public OnPlaceSettings onPlaceSettings;
        public ManhattanSettings manhattanSettings;
    }
}