using System.Collections;
using System.Collections.Generic;
using Rubik;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RubikMonobehaviour : SerializedMonoBehaviour
{
    [SerializeField] private string command;
    [SerializeField] private Image[] images;
    [SerializeField] private Color White;
    [SerializeField] private Color Orange;
    [SerializeField] private Color Green;
    [SerializeField] private Color Red;
    [SerializeField] private Color Blue;
    [SerializeField] private Color Yellow;
    
    private void OnEnable()
    {
        CreateRubik();
    }

    [Button]
    private void CreateRubik()
    {
        RubikCube rubikCube = new RubikCube(command);
        string rubik = rubikCube.ToString();
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(rubik.Substring(i * 9, 9));
        }
        for (int i = 0; i < rubik.Length; i++)
        {
            Color color = Color.black;
            if (rubik[i] == 'W')
                color = White;
            else if (rubik[i] == 'O')
                color = Orange;
            else if (rubik[i] == 'G')
                color = Green;
            else if (rubik[i] == 'R')
                color = Red;
            else if (rubik[i] == 'B')
                color = Blue;
            else if (rubik[i] == 'Y')
                color = Yellow;

            images[i].color = color;
        }
    }
}
