using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik
{
    [CreateAssetMenu(fileName = "StepByStepSettings", menuName = "HeuristicSettings/StepByStepSettings", order = 1)]
    public class StepByStepSettings : ScriptableObject
    {
        public float Cost = 1f;

        public int maxStep = 1;

        public int[] Map => new[]
        {
            8, 4, 7,
            1, 1, 3,
            5, 2, 6,//UP

            8, 1, 5,
            12, 1, 9,
            20, 13, 17,//LEFT

            5, 2, 6,
            9, 1, 10,
            17, 14, 18,//FRONT

            6, 3, 7,
            10, 1, 11,
            18, 15, 19,//RIGHT

            7, 4, 8,
            11, 1, 12,
            19, 16, 20,//BACK

            17, 14, 18,
            13, 1, 15,
            20, 16, 19,//DOWN
        };
    }
}
