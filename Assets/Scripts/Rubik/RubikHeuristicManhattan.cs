using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rubik
{
    public class RubikHeuristicManhattan : IRubikHeuristic
    {
        private ManhattanSettings settings;
        
        public float Heuristic(RubikCube cube, HeuristicSettings settings)
        {
            this.settings = settings.manhattanSettings;
            return GetHeuristicMap(cube).Sum() / cube.Cube.Length;
        }

        public float Cost(RubikCube node, RubikCube successor)
        {
            return settings.Cost;
        }
        
        public static float[] GetHeuristicMap(RubikCube cube)
        {
            float[] heuristicMap = new float[cube.Cube.Length];
            for (int i = 0; i < cube.Cube.Length; i++)
            {
                int end = RubikCube.CubeNum(cube.Cube[i].num);
                int curr = RubikCube.CubeNum(i);
                Vector3 endV = new Vector3(end / 9 - curr / 9, end % 3 - curr % 3, z: (end % 9) / 3 - (curr % 9) / 3);
                heuristicMap[i] = endV.magnitude / 4.0f;
            }

            return heuristicMap;
        }
    }
}