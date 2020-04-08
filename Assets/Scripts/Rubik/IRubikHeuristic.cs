namespace Rubik
{
    public interface IRubikHeuristic
    {
        float Heuristic(RubikCube cube);
        float Cost(RubikCube node, RubikCube successor);
    }
}