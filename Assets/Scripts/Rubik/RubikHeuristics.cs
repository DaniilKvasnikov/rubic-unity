using System.Collections.Generic;

namespace Rubik
{
    public enum HeuristicType
    {
        OnPlace,
        Manhattan 
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
            { HeuristicType.Manhattan, new RubikHeuristicManhattan() },
        };
    }
}