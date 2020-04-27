using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Rubik
{
    public class Rubik : SerializedMonoBehaviour
    {
        [SerializeField] private string command;
        [SerializeField] private string decision;
        [SerializeField] private SideMiniCube[] miniCubes;
        [SerializeField] private Dictionary<RubikSide, Color> colors = new Dictionary<RubikSide, Color>();
        
        [SerializeField] public HeuristicSettings settings;

        private RubikCube rubikCube;
        public RubikCube RubikCube => rubikCube ?? (rubikCube = new RubikCube(settings));

        public string Command => command;
        public string Decision { get=> decision; set => decision = value; }

        [Button]
        public void ResetCube()
        {
            RubikCube.Reset();
            DrawCube();
        }

        [Button]
        private void MixUpCube()
        {
            ResetCube();
            RubikCube.UseCommand(command);
            DrawCube();
        }

        [Button]
        private void UseDecision()
        {
            MixUpCube();
            RubikCube.UseDecision(decision);
            DrawCube();
        }


        [Button]
        public void RandomCommand(int len)
        {
            command = RubikCube.GetRandomCommands(len);
        }

        private void DrawCube()
        {
            var h = RubikHeuristicManhattan.GetHeuristicMap(RubikCube);
            for (int i = 0; i < RubikCube.Cube.Length; i++)
            {
                miniCubes[i].SetCube(this, RubikCube.Cube[i], h[i]);
            }
        }

        public Color GetColor(RubikSide rubikInfoSide)
        {
            return colors[rubikInfoSide];
        }
    }
}
