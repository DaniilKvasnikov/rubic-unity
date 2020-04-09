namespace Rubik
{
    public interface IRubikHeuristic
    {
        float Heuristic(RubikCube cube, HeuristicSettings settings);
        float Cost(RubikCube node, RubikCube successor);
    }
}