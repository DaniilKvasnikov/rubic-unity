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
    [SerializeField] private float TOLERANCE = .1f;
    
    private int angle = 90;
    
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
        foreach (var cube in generator.cubeElements)
        {
            switch (info.axis)
            {
                case Axis.X:
                    if (Math.Abs(cube.transform.position.x - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.right, angle);
                    break;
                case Axis.Y:
                    if (Math.Abs(cube.transform.position.y - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.up, angle);
                    break;
                case Axis.Z:
                    if (Math.Abs(cube.transform.position.z - info.num) < TOLERANCE)
                        cube.transform.RotateAround(center, Vector3.forward, angle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
