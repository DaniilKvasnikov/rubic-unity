﻿using Sirenix.OdinInspector;
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
        [SerializeField] private Color up = Color.white;
        [SerializeField] private Color left = Color.white;
        [SerializeField] private Color front = Color.white;
        [SerializeField] private Color right = Color.white;
        [SerializeField] private Color back = Color.white;
        [SerializeField] private Color down = Color.white;
        
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
            string rubik = RubikCube.ToString();
            for (int i = 0; i < rubik.Length; i++)
            {
                Color color = Color.black;
                if (rubik[i] == 'W')
                    color = up;
                else if (rubik[i] == 'O')
                    color = left;
                else if (rubik[i] == 'G')
                    color = front;
                else if (rubik[i] == 'R')
                    color = right;
                else if (rubik[i] == 'B')
                    color = back;
                else if (rubik[i] == 'Y')
                    color = down;

                images[i].color = color;
            }
        }
    }
}
