using System.Collections.Generic;

namespace Rubik
{
    public enum HeuristicType
    {
        OnPlace = 0,
        Manhattan = 1,
        StepByStep = 2,
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
            { HeuristicType.StepByStep, new RubikHeuristicStepByStep() },
        };
    }
}