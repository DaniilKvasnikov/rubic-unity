using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class CubikElement : SerializedMonoBehaviour
{
    public Dictionary<SideOfTheCube, GameObject> sides = new Dictionary<SideOfTheCube, GameObject>();
}

public enum SideOfTheCube
{
    Up,
    Left,
    Front,
    Right,
    Back,
    Down,
}
