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


        [HideInInspector] public RubikCube rubikCube = new RubikCube();
    
        [Button]
        public void ResetCube()
        {
            rubikCube.Reset();
            DrawCube();
        }

        [Button]
        private void MixUpCube()
        {
            rubikCube.UseCommands(CorrectCommand(command));
            DrawCube();
        }

        [Button]
        private void UseDecision()
        {
            rubikCube.UseCommands(CorrectCommand(decision));
            DrawCube();
        }


        [Button]
        public void RandomCommand(int len)
        {
            var newCommand = "";
            for (int i = 0; i < len; i++)
            {
                char randomCommand = RubikCube.Commands[Random.Range(0, RubikCube.Commands.Length - 1)];
                bool reverse = Random.Range(0, 1) == 1;
                newCommand += randomCommand + (reverse ? "\'" : "");
            }

            command = newCommand;
        }

        private void DrawCube()
        {
            string rubik = rubikCube.ToString();
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

        private string CorrectCommand(string command)
        {
            command = command.Replace(" ", "");
            if (command.Length == 0) return command;
            string newCommand = command[0].ToString();
            for (int i = 1; i < command.Length; i++)
            {
                newCommand += command[i] != '2' ? command[i] : command[i - 1];
            }
            return newCommand;
        }
    }
}
