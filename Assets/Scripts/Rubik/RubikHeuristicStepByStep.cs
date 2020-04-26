using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rubik
{
    public class RubikHeuristicStepByStep : IRubikHeuristic
    {
        private StepByStepSettings settings;
        private int step = 0;
        
        public float Heuristic(RubikCube cube, HeuristicSettings settings)
        {
            this.settings = settings.stepByStepSettings;
            return RubikHeuristicManhattan.GetHeuristicMap(cube).Sum() / cube.Cube.Length;
        }
        private void StepUpdate(RubikCube cube)
        {
            var map = GetErrorMap(cube);
            for (int i = step + 1; i < map.Length; i++)
            {
                if (map[i]) return;
                step++;
            }
        }

        private bool[] GetErrorMap(RubikCube cube)
        {
            var max = settings.Map.Max();
            var errorMap = new bool[max + 1];
            for (int i = 0; i < cube.Cube.Length; i++)
            {
                if (cube.Cube[i].num != i)
                {
                    errorMap[settings.Map[i]] = true;
                }
            }
            return errorMap;
        }

        public float Cost(RubikCube node, RubikCube successor)
        {
            return settings.Cost;
        }
    }
}