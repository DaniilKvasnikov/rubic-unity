using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik
{
    [CreateAssetMenu(fileName = "StepByStepSettings", menuName = "HeuristicSettings/StepByStepSettings", order = 1)]
    public class StepByStepSettings : ScriptableObject
    {
        public float Cost = 1f;

        public float maxStep = 3f;

        public int[] Map => new[]
        {
            2, 1, 2,
            1, 1, 1,
            2, 1, 2,//UP

            2, 1, 2,
            3, 1, 3,
            5, 4, 5,//LEFT

            2, 1, 2,
            3, 1, 3,
            5, 4, 5,//FRONT

            2, 1, 2,
            3, 1, 3,
            5, 4, 5,//RIGHT

            2, 1, 2,
            3, 1, 3,
            5, 4, 5,//BACK

            5, 4, 5,
            4, 1, 4,
            5, 4, 5,//DOWN
        };
    }
}
