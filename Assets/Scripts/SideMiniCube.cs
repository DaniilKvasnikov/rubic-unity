using System.Collections;
using System.Collections.Generic;
using Rubik;
using UnityEngine;
using UnityEngine.UI;

public class SideMiniCube : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    public void SetCube(Rubik.Rubik info, RubikInfo rubikInfo, float h)
    {
        image.color = info.GetColor(rubikInfo.side);
        text.text = rubikInfo.num + "/" + h;
    }
}
