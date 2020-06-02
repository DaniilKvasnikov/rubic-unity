using System;
using System.Collections;
using System.Collections.Generic;
using Rubik;
using UnityEngine;
using UnityEngine.UI;

public class SideMiniCube : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    public void SetCube(Func<RubikSide, Color> getColor, RubikInfo rubikInfo, float h)
    {
        image.color = getColor(rubikInfo.side);
        text.text = rubikInfo.num + "/" + h;
    }
}
