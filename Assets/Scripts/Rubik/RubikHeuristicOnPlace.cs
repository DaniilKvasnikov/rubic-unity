using System.Linq;

namespace Rubik
{
    public class RubikHeuristicOnPlace : IRubikHeuristic
    {
        public float Heuristic(RubikCube cube)
        {
            return HeuristicNotOnPlace(cube);
        }

        private float HeuristicNotOnPlace(RubikCube cube)
        {
            return cube.Cube.Select((t, i) => (t.num != i ? 1 : 0)).Sum();
        }
        
        public float Cost(RubikCube node, RubikCube successor)
        {
            return 20f;
        }
    }
}