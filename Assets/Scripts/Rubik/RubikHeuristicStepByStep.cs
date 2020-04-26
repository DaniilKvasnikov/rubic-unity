using System;
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
            StepUpdate(cube);
            var heuristicMap = GetHeuristicMap(RubikHeuristicManhattan.GetHeuristicMap(cube));
            return heuristicMap.Sum();
        }

        private float[] GetHeuristicMap(float[] fullHeuristicMap)
        {
            float[] heuristicMap = new float[fullHeuristicMap.Length];
            // for (int i = 0; i < fullHeuristicMap.Length; i++)
            // {
            //     if (settings.Map[i] > step + 1)
            //         heuristicMap[i] = settings.maxStep;
            // }

            return heuristicMap;
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