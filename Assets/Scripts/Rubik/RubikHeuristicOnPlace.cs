using System.Linq;

namespace Rubik
{
    public class RubikHeuristicOnPlace : IRubikHeuristic
    {
        private OnPlaceSettings settings;
        
        public float Heuristic(RubikCube cube, HeuristicSettings settings)
        {
            this.settings = settings.onPlaceSettings;
            return HeuristicNotOnPlace(cube);
        }

        private float HeuristicNotOnPlace(RubikCube cube)
        {
            return cube.Cube.Select((t, i) => (t.num != i ? 1 : 0)).Sum();
        }
        
        public float Cost(RubikCube node, RubikCube successor)
        {
            return settings.Cost;
        }
    }
}