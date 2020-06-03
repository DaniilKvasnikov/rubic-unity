using System;
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
        [SerializeField] private Image[] images;
        [SerializeField] private Dictionary<RubikSide, Color> colors;
        
        public RubikCube RubikCube { get; } = new RubikCube();
        
        public string Command
        {
            get => command;
            set => command = value;
        }

        public string Decision
        {
            get=> decision;
            set => decision = value;
        }

        private void OnEnable()
        {
            ResetCube();
        }

        [Button]
        public void ResetCube()
        {
            RubikCube.Reset();
            DrawCube();
        }

        [Button]
        public void MixUpCube()
        {
            ResetCube();
            RubikCube.UseCommand(command);
            DrawCube();
        }

        [Button]
        public void UseDecision()
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
            for (int i = 0; i < RubikCube.Cube.Length; i++)
            {
                images[i].color = colors[RubikCube.Cube[i].side];
            }
        }
    }
}
