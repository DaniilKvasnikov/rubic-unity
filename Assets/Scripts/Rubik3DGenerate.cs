using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Rubik3DGenerate : MonoBehaviour
{
    [SerializeField] private GameObject prefabCube;
    
    private int size = 3;

    [Button]
    private void Generate()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    var cube = Instantiate(prefabCube, transform);
                    cube.transform.localPosition = new Vector3(x, y, z);
                    var element = cube.GetComponent<CubikElement>();
                    if (element == null) throw new Exception("CubikElement not found");
                    element.sides[SideOfTheCube.Up].SetActive(y == size - 1);
                    element.sides[SideOfTheCube.Left].SetActive(x == 0);
                    element.sides[SideOfTheCube.Front].SetActive(z == 0);
                    element.sides[SideOfTheCube.Right].SetActive(x == size - 1);
                    element.sides[SideOfTheCube.Back].SetActive(z == size - 1);
                    element.sides[SideOfTheCube.Down].SetActive(y == 0);
                }
            }
        }
    }
}
