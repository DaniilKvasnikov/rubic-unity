namespace Rubik
{
    public class RubikHeuristicManhattan : IRubikHeuristic
    {
        private ManhattanSettings settings;
        
        public float Heuristic(RubikCube cube, HeuristicSettings settings)
        {
            this.settings = settings.manhattanSettings;
            float heuristic = 0;
            for (int i = 0; i < cube.Cube.Length; i++)
            {
                int endFace = cube.Cube[i].num / 9;
                int endPlace = cube.Cube[i].num % 9;
                int currFace = i / 9;
                int currPlace = i % 9;
                int faceRotate = endFace != currFace ? 1 : 0;
                int placeRotate = (endPlace != currPlace) ? 1 : 0;
                heuristic += faceRotate + placeRotate;
            }

            return heuristic / cube.Cube.Length;
        }

        public float Cost(RubikCube node, RubikCube successor)
        {
            return settings.Cost;
        }
    }
}