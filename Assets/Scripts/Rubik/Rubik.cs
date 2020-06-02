using System;
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
        [SerializeField] private Color up = Color.white;
        [SerializeField] private Color left = Color.white;
        [SerializeField] private Color front = Color.white;
        [SerializeField] private Color right = Color.white;
        [SerializeField] private Color back = Color.white;
        [SerializeField] private Color down = Color.white;
        
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
            string rubik = RubikCube.ToString();
            Debug.Log(rubik);
            for (int i = 0; i < 6; i++)
            {
                Debug.Log(rubik.Substring(i * 9, 9));
            }
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
