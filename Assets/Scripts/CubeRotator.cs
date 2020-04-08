using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class CubeRotator : SerializedMonoBehaviour
{
    [SerializeField] private KeyCode reverseButton = KeyCode.LeftShift;
    [OdinSerialize] public Dictionary<KeyCode, RotateInfo> rotateButton = new Dictionary<KeyCode, RotateInfo>();
    [SerializeField] private RubikGenerator generator;
    
    void Update()
    {
        if (Input.GetKeyDown(reverseButton))
            reverse = !reverse;
        
        foreach (var button in rotateButton)
        {
            if (Input.GetKeyDown(button.Key))
            {
                ButtonPress(button.Value, reverse);
            }
        }
    }

    private void ButtonPress(RotateInfo info, bool reverse)
    {
        Vector3 center = new Vector3(1, 1, 1);
        float mulReverse = reverse ? -1f : 1f;
        foreach (var cube in generator.cubeElements)
        {
            switch (info.axis)
            {
                case Axis.X:
                    if (Math.Abs(cube.transform.position.x - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.right * mulReverse, angle);
                    break;
                case Axis.Y:
                    if (Math.Abs(cube.transform.position.y - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.up * mulReverse, angle);
                    break;
                case Axis.Z:
                    if (Math.Abs(cube.transform.position.z - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.forward * mulReverse, angle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void OnGUI()
    {
        int i = 1;
        var text = "Reverse " + (reverse ? "on" : "off");
        if (GUI.Button(new Rect(10, 10, 100, 20), text))
            reverse = !reverse;
        foreach (var info in rotateButton)
        {
            text = info.Key + " ";
            text += info.Value.axis + " ";
            text += info.Value.num;
            if (GUI.Button(new Rect(10, 10 + i * 25, 100, 20), text))
            {
                ButtonPress(info.Value, reverse);
            }
            i++;
        }
    }
}
