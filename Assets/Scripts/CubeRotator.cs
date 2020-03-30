using System;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class CubeRotator : SerializedMonoBehaviour
{
    [OdinSerialize] public Dictionary<KeyCode, RotateInfo> rotateButton = new Dictionary<KeyCode, RotateInfo>();
    [SerializeField] private RubicGenerator generator;
    
    void Update()
    {
        foreach (var button in rotateButton)
        {
            if (Input.GetKeyDown(button.Key))
            {
                ButtonPress(button.Value);
            }
        }
    }

    private void ButtonPress(RotateInfo info)
    {
        Vector3 center = new Vector3(1, 1, 1);
        Vector3 xAxis = new Vector3(1, 0, 0);
        Vector3 yAxis = new Vector3(0, 1, 0);
        Vector3 zAxis = new Vector3(0, 0, 1);
        int angle = 90;
        foreach (var cube in generator.cubes)
        {
            switch (info.axis)
            {
                case Axis.X:
                    if (cube.transform.position.x == info.num)
                        cube.transform.RotateAround(center, xAxis, angle);
                    break;
                case Axis.Y:
                    if (cube.transform.position.y == info.num)
                        cube.transform.RotateAround(center, yAxis, angle);
                    break;
                case Axis.Z:
                    if (cube.transform.position.z == info.num)
                        cube.transform.RotateAround(center, zAxis, angle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
