using System.Collections.Generic;

namespace Rubik
{
    public enum HeuristicType
    {
        OnPlace
    }
    
    public static class RubikHeuristics
    {
        public static IRubikHeuristic GetHeuristic(HeuristicType heuristicType)
        {
            return Heuristics[heuristicType];
        }
        
        private static readonly Dictionary<HeuristicType, IRubikHeuristic> Heuristics = new Dictionary<HeuristicType, IRubikHeuristic>()
        {
            { HeuristicType.OnPlace, new RubikHeuristicOnPlace() },
        };
    }
}